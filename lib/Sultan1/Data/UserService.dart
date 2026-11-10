import 'dart:convert';
import 'package:http/http.dart' as http;

import 'conection.dart';

class UserService {

  Future<Map<String, dynamic>?> login(
      String username,
      String password
      ) async {

    final response = await http.post(
      Uri.parse(ConectionString.login),

      headers: {
        'Content-Type': 'application/json',
      },

      body: json.encode({
        'username': username,
        'password': password,
      }),
    );

    if (response.statusCode == 200) {

      return json.decode(response.body);

    } else if (response.statusCode == 401) {

      return null;

    } else {

      throw Exception(
        'Failed to login: ${response.statusCode}',
      );
    }
  }

  Future<Map<String, dynamic>?> register(
      String gender,
      String email,
      String username,
      String password
      ) async {

    final response = await http.post(
      Uri.parse(ConectionString.register),

      headers: {
        'Content-Type': 'application/json',
      },

      body: json.encode({
        'gender': gender,
        'email': email,
        'username': username,
        'password': password,
      }),
    );

    if (response.statusCode == 200) {

      return json.decode(response.body);

    } else if (
    response.statusCode == 400 ||
        response.statusCode == 409
    ) {

      return null;

    } else {

      throw Exception(
        'Registration failed: ${response.statusCode} - ${response.body}',
      );
    }
  }
}