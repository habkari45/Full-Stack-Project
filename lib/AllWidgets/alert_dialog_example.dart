import 'package:flutter/material.dart';

class Alertdialog extends StatelessWidget {
  const Alertdialog({super.key});

  void _showDialog(BuildContext context, String title) {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          //backgroundColor: Colors.amber,
          title: Row(
            children: [
              const Icon(
                Icons.warning,
                color: Colors.red,
              ),
              const SizedBox(
                width: 10,
              ),
              Text(
                title,
                style: const TextStyle(color: Colors.blue),
              ),
            ],
          ),
          content: const Text('this  is alert dialog'),
          actions: [
            TextButton(
              onPressed: () => Navigator.of(context).pop(),
              child: const Text('OK'),
            ),
            TextButton(
              onPressed: () => Navigator.of(context).pop(),
              child: const Text('Cancel'),
            ),
            // IconButton(onPressed: () {}, icon: Icon(Icons.add))
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('AlertDialog Example'),
        backgroundColor: Colors.blue,
      ),
      body: Center(
        child: ElevatedButton(
          onPressed: () => _showDialog(context, 'Alert'),
          child: const Text('Show Alert'),
        ),
      ),
    );
  }
}