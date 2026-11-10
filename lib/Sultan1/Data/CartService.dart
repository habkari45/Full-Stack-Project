import 'dart:convert';
import 'package:http/http.dart' as http;

import 'conection.dart';

class CartService {

  // GET CART ITEMS
  Future<List<dynamic>> getCart(int userId) async {

    final response = await http.get(
      Uri.parse(
        '${ConectionString.cart}/GetCartItems/$userId',
      ),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body) as List<dynamic>;
    }

    throw Exception('Failed to load cart');
  }

  // ADD TO CART
  Future<void> addToCart(
      int userId,
      int productId,
      ) async {

    final response = await http.post(
      Uri.parse(
        '${ConectionString.cart}/AddToCart',
      ),

      headers: {
        'Content-Type': 'application/json',
      },

      body: jsonEncode({
        'userId': userId,
        'productId': productId,
        'quantity': 1,
      }),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to add product');
    }
  }

  // REMOVE FROM CART
  Future<void> removeFromCart(
      int userId,
      int productId,
      ) async {

    final response = await http.delete(
      Uri.parse(
        '${ConectionString.cart}/RemoveFromCart/$userId/$productId',
      ),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to remove product');
    }
  }
}