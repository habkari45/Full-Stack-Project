import 'dart:convert';
import 'package:http/http.dart' as http;

import 'conection.dart';

class OrderService {
  // CREATE ORDER
  Future<int> makeOrder(int userId) async {
    final response = await http.post(
      Uri.parse('${ConectionString.orders}/$userId'),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);

      return (data['orderId'] as num).toInt();
    }

    throw Exception(
      'Failed to create order: ${response.body}',
    );
  }

  // GET USER ORDERS
  Future<List<dynamic>> getMyOrders(int userId) async {
    final response = await http.get(
      Uri.parse('${ConectionString.orders}/user/$userId'),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body) as List<dynamic>;
    }

    throw Exception(
      'Failed to load orders: ${response.body}',
    );
  }
}