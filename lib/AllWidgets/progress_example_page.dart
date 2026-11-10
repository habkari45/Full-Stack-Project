import 'dart:async';

import 'package:flutter/material.dart';

class ProgressExamplePage extends StatefulWidget {
  const ProgressExamplePage({super.key});

  @override
  State<ProgressExamplePage> createState() => _ProgressExamplePageState();
}

class _ProgressExamplePageState extends State<ProgressExamplePage> {
  double progressValue = 0.0;
  Timer? timer;

  @override
  void initState() {
    super.initState();

    // مؤقت بسيط يزود القيمة عشان نشوف الـ determinate شغال
    timer = Timer.periodic(const Duration(milliseconds: 300), (t) {
      setState(() {
        progressValue += 0.1;
        if (progressValue > 1) {
          progressValue = 0.0; // نرجع من أول
        }
      });
    });
  }

  @override
  void dispose() {
    timer?.cancel();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Progress Indicators'),
        backgroundColor: Colors.blue,
      ),
      body: Padding(
        padding: const EdgeInsets.all(20.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              // 1) CircularProgressIndicator - غير محدد
              const Text(
                '1. CircularProgressIndicator (بدون نسبة - تحميل مستمر)',
                style: TextStyle(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),
              const Center(
                child: CircularProgressIndicator(),
              ),
              const SizedBox(height: 30),

              // 2) CircularProgressIndicator - بنسبة
              const Text(
                '2. CircularProgressIndicator بنسبة (value)',
                style: TextStyle(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),
              Center(
                child: Column(
                  children: [
                    CircularProgressIndicator(
                      value: progressValue,
                      strokeWidth: 8,
                      backgroundColor: Colors.grey.shade300,
                    ),
                    const SizedBox(height: 8),
                    Text('${(progressValue * 100).toStringAsFixed(0)}%'),
                  ],
                ),
              ),
              const SizedBox(height: 30),

              // 3) LinearProgressIndicator - غير محدد
              const Text(
                '3. LinearProgressIndicator (بدون نسبة - شريط تحميل)',
                style: TextStyle(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),
              const LinearProgressIndicator(),
              const SizedBox(height: 30),

              // 4) LinearProgressIndicator - بنسبة
              const Text(
                '4. LinearProgressIndicator بنسبة (value)',
                style: TextStyle(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  LinearProgressIndicator(
                    value: progressValue,
                    minHeight: 10,
                    backgroundColor: Colors.grey.shade300,
                  ),
                  const SizedBox(height: 8),
                  Text(
                    'Loaded: ${(progressValue * 100).toStringAsFixed(0)}%',
                  ),
                ],
              ),
              const SizedBox(height: 30),

              // 5) LinearProgressIndicator داخل Card
              const Text(
                '5. Progress داخل Card (تصميم شائع في الداشبورد)',
                style: TextStyle(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),
              Card(
                elevation: 2,
                child: Padding(
                  padding: const EdgeInsets.all(16.0),
                  child: Row(
                    children: [
                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            const Text('رفع الملف...'),
                            const SizedBox(height: 8),
                            LinearProgressIndicator(
                              value: progressValue,
                              minHeight: 8,
                              backgroundColor: Colors.grey.shade300,
                            ),
                            const SizedBox(height: 8),
                            Text(
                              '${(progressValue * 100).toStringAsFixed(0)}%',
                              style: const TextStyle(fontSize: 12),
                            ),
                          ],
                        ),
                      ),
                      const SizedBox(width: 12),
                      const Icon(Icons.upload_file),
                    ],
                  ),
                ),
              ),
              const SizedBox(height: 30),

              // 6) CircularProgressIndicator صغير كأيقونة حالة
              const Text(
                '6. أيقونة حالة تحميل صغيرة (مثلاً جنب زر)',
                style: TextStyle(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),
              Row(
                children: [
                  ElevatedButton(
                    onPressed: () {},
                    child: const Text('حفظ'),
                  ),
                  const SizedBox(width: 16),
                  const SizedBox(
                    height: 20,
                    width: 20,
                    child: CircularProgressIndicator(strokeWidth: 3),
                  ),
                  const SizedBox(width: 8),
                  const Text('جاري الحفظ...'),
                ],
              ),

              const SizedBox(height: 50),
            ],
          ),
        ),
      ),
    );
  }
}