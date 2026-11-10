import 'package:flutter/material.dart';

class Listviewexample extends StatelessWidget {
  Listviewexample({super.key});

  final List<String> items =
  List<String>.generate(10, (index) => "Item ${index + 1}");

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('ListView Example'),
        backgroundColor: Colors.blue,
      ),
      body: ListView.builder(
        itemCount: items.length,
        itemBuilder: (context, index) {
          return Text(items[index]);
        },
      ),
    );
  }
}