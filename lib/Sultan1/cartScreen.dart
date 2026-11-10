import 'package:flutter/material.dart';

import 'data/CartService.dart';
import 'data/OrderService.dart';
import 'data/conection.dart';
import 'myOrderScreen.dart';


class CartScreen extends StatefulWidget {
  const CartScreen(this.userId, {super.key});

  final int userId;

  @override
  State<CartScreen> createState() => _CartScreenState();
}

class _CartScreenState extends State<CartScreen> {
  final CartService cartService = CartService();
  final OrderService orderService = OrderService();

  late Future<List<dynamic>> cartItems;

  bool isOrdering = false;

  // LOAD CART
  void loadCart() {
    cartItems = cartService.getCart(widget.userId);
  }

  @override
  void initState() {
    super.initState();
    loadCart();
  }

  // DELETE PRODUCT
  Future<void> deleteProduct(int productId) async {
    try {
      await cartService.removeFromCart(
        widget.userId,
        productId,
      );

      if (!mounted) return;

      setState(() {
        loadCart();
      });

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Product removed from cart'),
          backgroundColor: Colors.green,
        ),
      );
    } catch (e) {
      if (!mounted) return;

      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Error: $e'),
          backgroundColor: Colors.red,
        ),
      );
    }
  }

  // MAKE ORDER
  Future<void> makeOrder() async {
    if (isOrdering) return;

    setState(() {
      isOrdering = true;
    });

    try {
      final int orderId = await orderService.makeOrder(
        widget.userId,
      );

      if (!mounted) return;

      setState(() {
        loadCart();
      });

      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(
            'Order #$orderId created successfully',
          ),
          backgroundColor: Colors.green,
        ),
      );

      Navigator.push(
        context,
        MaterialPageRoute(
          builder: (context) =>
              MyOrdersScreen(widget.userId),
        ),
      );
    } catch (e) {
      if (!mounted) return;

      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Failed to create order: $e'),
          backgroundColor: Colors.red,
        ),
      );
    } finally {
      if (mounted) {
        setState(() {
          isOrdering = false;
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('My Cart'),
        backgroundColor: Colors.indigo,
      ),

      body: FutureBuilder<List<dynamic>>(
        future: cartItems,

        builder: (context, snapshot) {
          if (snapshot.connectionState ==
              ConnectionState.waiting) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (snapshot.hasError) {
            return Center(
              child: Text(
                'Error: ${snapshot.error}',
              ),
            );
          }

          final items = snapshot.data ?? [];

          if (items.isEmpty) {
            return const Center(
              child: Text(
                'Your Cart is Empty',
                style: TextStyle(fontSize: 20),
              ),
            );
          }

          double total = 0;

          for (var item in items) {
            total +=
                (item['price'] as num).toDouble() *
                    (item['quantity'] as num).toInt();
          }

          return Column(
            children: [
              Expanded(
                child: ListView.builder(
                  itemCount: items.length,

                  itemBuilder: (context, index) {
                    final item = items[index];

                    final String imagePath =
                        item['imageUrl']?.toString() ?? '';

                    final String imageUrl =
                    imagePath.isEmpty
                        ? ''
                        : Uri.parse(
                      ConectionString.dashboardUrl,
                    ).resolve(imagePath).toString();

                    return Card(
                      margin: const EdgeInsets.all(10),

                      child: ListTile(
                        leading: SizedBox(
                          width: 65,
                          height: 65,

                          child: imageUrl.isNotEmpty
                              ? Image.network(
                            imageUrl,
                            fit: BoxFit.cover,

                            errorBuilder:
                                (context, error, stackTrace) {
                              return const Icon(Icons.image);
                            },
                          )
                              : const Icon(Icons.image),
                        ),

                        title: Text(
                          item['productName']?.toString() ?? '',
                        ),

                        subtitle: Text(
                          '${item['price']} SAR\n'
                              'Quantity: ${item['quantity']}\n'
                              'Total: ${((item['price'] as num) * (item['quantity'] as num)).toStringAsFixed(2)} SAR',
                        ),

                        isThreeLine: true,

                        trailing: IconButton(
                          icon: const Icon(
                            Icons.delete,
                            color: Colors.red,
                          ),

                          onPressed: isOrdering
                              ? null
                              : () {
                            deleteProduct(
                              (item['productId'] as num).toInt(),
                            );
                          },
                        ),
                      ),
                    );
                  },
                ),
              ),

              Container(
                padding: const EdgeInsets.all(20),
                width: double.infinity,

                child: Column(
                  children: [
                    Text(
                      'Total: ${total.toStringAsFixed(2)} SAR',
                      textAlign: TextAlign.center,

                      style: const TextStyle(
                        fontSize: 23,
                        fontWeight: FontWeight.bold,
                        color: Colors.green,
                      ),
                    ),

                    const SizedBox(height: 15),

                    SizedBox(
                      width: double.infinity,

                      child: ElevatedButton.icon(
                        onPressed:
                        isOrdering ? null : makeOrder,

                        icon: isOrdering
                            ? const SizedBox(
                          width: 18,
                          height: 18,
                          child: CircularProgressIndicator(
                            strokeWidth: 2,
                          ),
                        )
                            : const Icon(Icons.shopping_bag),

                        label: Text(
                          isOrdering
                              ? 'Creating Order...'
                              : 'Make Order',
                        ),

                        style: ElevatedButton.styleFrom(
                          backgroundColor: Colors.indigo,
                          foregroundColor: Colors.white,
                          padding: const EdgeInsets.all(15),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ],
          );
        },
      ),
    );
  }
}