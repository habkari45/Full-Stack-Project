import 'package:flutter/material.dart';

class MyTextFormField extends StatelessWidget {
  final String name;
  final bool isPassword;

  const MyTextFormField({
    super.key,
    required this.name,
    this.isPassword = false,
  });

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      obscureText: isPassword,
      decoration: InputDecoration(
        labelText: name,
        labelStyle: const TextStyle(
          color: Colors.blue,
        ),
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(20),
          borderSide: const BorderSide(
            color: Colors.blue,
            width: 2,
          ),
        ),
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(20),
          borderSide: const BorderSide(
            color: Colors.blue,
            width: 2,
          ),
        ),
      ),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return "$name is required";
        }
        return null;
      },
    );
  }
}