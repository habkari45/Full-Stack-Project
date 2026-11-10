import 'package:flutter/material.dart';

class Listviewexample2 extends StatelessWidget {
  Listviewexample2({super.key});

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
            return Container(
              padding: const EdgeInsets.symmetric(vertical: 5, horizontal: 10),
              child: ListTile(
                leading: const Icon(
                  Icons.label,
                  color: Colors.blue,
                ),
                title: Text(items[index]),
                subtitle: Text(items[index]),
                trailing: const Icon(Icons.video_label_outlined),
                tileColor: Colors.amber,
                shape:
                OutlineInputBorder(borderRadius: BorderRadius.circular(20)),
              ),
            );
          }),
    );
  }
}