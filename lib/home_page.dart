import 'package:flutter/material.dart';

import 'Sultan1/myOrderScreen.dart';
import 'Sultan1/productScreen.dart';
import 'Sultan1/cartScreen.dart';
import 'profile_page.dart';
import 'Sultan1/login_Page3.dart';

class HomePage extends StatelessWidget {
  final String? username;
  final String? email;
  final int? userId;

  final String? gender;
  final DateTime? dateOfBirth;

  const HomePage({
    super.key,
    this.username,
    this.email,
    this.userId,
    this.gender,
    this.dateOfBirth,
  });

  // OPEN PRODUCTS
  void openProducts(BuildContext context) {
    Navigator.pop(context);

    if (userId == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Please login to view products'),
        ),
      );
      return;
    }

    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => ProductsScreen(userId!),
      ),
    );
  }

  // OPEN CART
  void openCart(BuildContext context) {
    Navigator.pop(context);

    if (userId == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Please login to view your cart'),
        ),
      );
      return;
    }

    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => CartScreen(userId!),
      ),
    );
  }

  // OPEN MY ORDERS
  void openOrders(BuildContext context) {
    Navigator.pop(context);

    if (userId == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Please login to view your orders'),
        ),
      );
      return;
    }

    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => MyOrdersScreen(userId!),
      ),
    );
  }

  // OPEN PROFILE
  void openProfile(BuildContext context) {
    Navigator.pop(context);

    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => ProfilePage(
          username: username,
          email: email,
          userId: userId,
          gender: gender,
          dateOfBirth: dateOfBirth,
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,

      appBar: AppBar(
        title: const Text(
          'Home Page',
          style: TextStyle(color: Colors.white),
        ),

        backgroundColor: Colors.blue,

        iconTheme: const IconThemeData(
          color: Colors.white,
        ),
      ),

      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,

          children: [
            Text(
              'Gender: ${gender ?? "Not Selected"}',
            ),

            Text(
              'Date of Birth: ${dateOfBirth == null ? "Not Selected" : "${dateOfBirth!.day}/${dateOfBirth!.month}/${dateOfBirth!.year}"}',
            ),
          ],
        ),
      ),

      // LEFT DRAWER
      drawer: Drawer(
        child: Column(
          children: [
            UserAccountsDrawerHeader(
              accountName: Text(
                username ?? 'Sultan Portfolio',
              ),

              accountEmail: Text(
                email ?? 'No Email',
              ),

              currentAccountPicture: const CircleAvatar(
                backgroundColor: Colors.white,

                child: Icon(
                  Icons.person,
                  size: 40,
                  color: Colors.blue,
                ),
              ),

              decoration: const BoxDecoration(
                color: Colors.blue,
              ),
            ),

            // HOME
            ListTile(
              leading: const Icon(
                Icons.home,
                color: Colors.blue,
              ),

              title: const Text('Home'),

              onTap: () {
                Navigator.pop(context);
              },
            ),

            // PROFILE
            ListTile(
              leading: const Icon(
                Icons.person,
                color: Colors.blue,
              ),

              title: const Text('Profile'),

              onTap: () {
                openProfile(context);
              },
            ),

            // PRODUCTS
            ListTile(
              leading: const Icon(
                Icons.shopping_bag,
                color: Colors.blue,
              ),

              title: const Text('Products'),

              onTap: () {
                openProducts(context);
              },
            ),

            // CART
            ListTile(
              leading: const Icon(
                Icons.shopping_cart,
                color: Colors.blue,
              ),

              title: const Text('My Cart'),

              onTap: () {
                openCart(context);
              },
            ),

            // MY ORDERS
            ListTile(
              leading: const Icon(
                Icons.receipt_long,
                color: Colors.blue,
              ),

              title: const Text('My Orders'),

              onTap: () {
                openOrders(context);
              },
            ),

            const Spacer(),

            // LOGOUT
            ListTile(
              leading: const Icon(
                Icons.logout,
                color: Colors.red,
              ),

              title: const Text(
                'Logout',
                style: TextStyle(color: Colors.red),
              ),

              onTap: () {
                Navigator.pushAndRemoveUntil(
                  context,
                  MaterialPageRoute(
                    builder: (context) =>
                    const Loginscreen3(),
                  ),
                      (route) => false,
                );
              },
            ),
          ],
        ),
      ),

      // RIGHT DRAWER
      endDrawer: Drawer(
        child: Column(
          children: [
            UserAccountsDrawerHeader(
              accountName: Text(
                username ?? 'Sultan Portfolio',
              ),

              accountEmail: Text(
                email ?? 'No Email',
              ),

              currentAccountPicture: const CircleAvatar(
                backgroundColor: Colors.white,

                child: Icon(
                  Icons.person,
                  color: Colors.blue,
                  size: 40,
                ),
              ),

              decoration: const BoxDecoration(
                color: Colors.blue,
              ),
            ),

            // HOME
            ListTile(
              leading: const Icon(
                Icons.home,
                color: Colors.blue,
              ),

              title: const Text('Home'),

              onTap: () {
                Navigator.pop(context);
              },
            ),

            // PROFILE
            ListTile(
              leading: const Icon(
                Icons.person,
                color: Colors.blue,
              ),

              title: const Text('Profile'),

              onTap: () {
                openProfile(context);
              },
            ),

            // PRODUCTS
            ListTile(
              leading: const Icon(
                Icons.shopping_bag,
                color: Colors.blue,
              ),

              title: const Text('Products'),

              onTap: () {
                openProducts(context);
              },
            ),

            // CART
            ListTile(
              leading: const Icon(
                Icons.shopping_cart,
                color: Colors.blue,
              ),

              title: const Text('My Cart'),

              onTap: () {
                openCart(context);
              },
            ),

            // MY ORDERS
            ListTile(
              leading: const Icon(
                Icons.receipt_long,
                color: Colors.blue,
              ),

              title: const Text('My Orders'),

              onTap: () {
                openOrders(context);
              },
            ),
          ],
        ),
      ),
    );
  }
}