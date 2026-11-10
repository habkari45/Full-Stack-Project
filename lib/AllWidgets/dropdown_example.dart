import 'package:flutter/material.dart';

class DropdownExample extends StatefulWidget {
  const DropdownExample({super.key});

  @override
  State<DropdownExample> createState() => _DropdownExampleState();
}

class _DropdownExampleState extends State<DropdownExample> {
  String? _selectedValue;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: const Text('Drop Down Example'),
          backgroundColor: Colors.blue,
        ),
        body: Center(
          child: DropdownButton<String>(
            hint: const Text('Select option'),
            value: _selectedValue,
            items: <String>['option1', 'option2', 'option3']
                .map<DropdownMenuItem<String>>((String value) {
              return DropdownMenuItem(value: value, child: Text(value));
            }).toList(),
            onChanged: (String? value) {
              setState(() {
                _selectedValue = value;
              });
            },
          ),
        ));
  }
}