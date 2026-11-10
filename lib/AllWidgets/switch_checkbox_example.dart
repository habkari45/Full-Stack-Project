import 'package:flutter/material.dart';

class SwitchcheckbocExample extends StatefulWidget {
  const SwitchcheckbocExample({super.key});

  @override
  State<SwitchcheckbocExample> createState() => _SwitchcheckbocexampleState();
}

class _SwitchcheckbocexampleState extends State<SwitchcheckbocExample> {
  bool _isChecked = false;
  bool _isSwitch = true;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Swtch Example'),
        backgroundColor: Colors.blue,
      ),
      body: Center(
        child: Column(
          children: [
            Checkbox(
              value: _isChecked,
              onChanged: (value) {
                setState(() {
                  _isChecked = value!;
                  print(_isChecked);
                });
              },
            ),
            Switch(
              value: _isSwitch,
              onChanged: (value) {
                setState(() {
                  _isSwitch = value;
                  print(_isSwitch);
                });
              },
            ),
          ],
        ),
      ),
    );
  }
}