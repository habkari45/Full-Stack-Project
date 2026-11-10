import 'package:flutter/material.dart';

class Gridviewexample extends StatelessWidget {
  const Gridviewexample({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Gridview Example'),
        backgroundColor: Colors.blue,
      ),
      body: GridView.count(
        mainAxisSpacing: 10,
        crossAxisSpacing: 10,
        crossAxisCount: 2,
        children: List.generate(16, (index) {
          return Card(
            elevation: 10,
            color: Colors.amber,
            child: Center(
              child: Column(
                children: [
                  Icon(Icons.apple),
                  SizedBox(height: 10),
                  Text('Item ${index + 1}'),
                  Text('Item ${index + 1}'),
                ],
              ),
            ),
          );
        }),
      ),
    );
  }
}