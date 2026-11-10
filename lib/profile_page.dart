import 'package:flutter/material.dart';

import 'my_text_form_field.dart';
import 'home_page.dart';
import 'Sultan1/login_Page3.dart';

class ProfilePage extends StatelessWidget {
  final String? username;
  final String? email;
  final int? userId;
  final String? gender;
  final DateTime? dateOfBirth;

  const ProfilePage({
    super.key,
    this.username,
    this.email,
    this.userId,
    this.gender,
    this.dateOfBirth,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'My Profile',
          style: TextStyle(color: Colors.white),
        ),
        backgroundColor: Colors.blue,
        iconTheme: const IconThemeData(color: Colors.white),
      ),

      drawer: Drawer(
        child: Column(
          children: [
            UserAccountsDrawerHeader(
              accountName: Text(username ?? 'Sultan Portfolio'),
              accountEmail: Text(email ?? 'No Email'),
              currentAccountPicture: const CircleAvatar(
                backgroundColor: Colors.white,
                child: Icon(
                  Icons.person,
                  size: 40,
                  color: Colors.blue,
                ),
              ),
              decoration: const BoxDecoration(
                color: Colors.blue,
              ),
            ),

            ListTile(
              leading: const Icon(Icons.home, color: Colors.blue),
              title: const Text('Home'),
              onTap: () {
                Navigator.pushReplacement(
                  context,
                  MaterialPageRoute(
                    builder: (context) => HomePage(
                      username: username,
                      email: email,
                      userId: userId,
                      gender: gender,
                      dateOfBirth: dateOfBirth,
                    ),
                  ),
                );
              },
            ),

            ListTile(
              leading: const Icon(Icons.person, color: Colors.blue),
              title: const Text('Profile'),
              onTap: () {
                Navigator.pop(context);
              },
            ),

            const Spacer(),

            ListTile(
              leading: const Icon(Icons.logout, color: Colors.red),
              title: const Text(
                'Logout',
                style: TextStyle(color: Colors.red),
              ),
              onTap: () {
                Navigator.pushAndRemoveUntil(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const Loginscreen3(),
                  ),
                      (route) => false,
                );
              },
            ),
          ],
        ),
      ),

      body: SingleChildScrollView(
        padding: const EdgeInsets.all(20),
        child: Column(
          children: [
            const CircleAvatar(
              radius: 50,
              child: Icon(Icons.person, size: 50),
            ),

            const SizedBox(height: 15),

            Text(
              username ?? 'Sultan',
              style: const TextStyle(
                fontSize: 24,
                fontWeight: FontWeight.bold,
              ),
            ),

            const SizedBox(height: 10),

            Text('Email: ${email ?? "Not Available"}'),

            const SizedBox(height: 10),

            Text('Gender: ${gender ?? "Not Selected"}'),

            const SizedBox(height: 10),

            Text(
              'Date of Birth: ${dateOfBirth == null ? "Not Selected" : "${dateOfBirth!.day}/${dateOfBirth!.month}/${dateOfBirth!.year}"}',
            ),

            const SizedBox(height: 15),

            const Text('Network Engineer'),
            const Text('Web Developer'),
            const Text('Flutter Developer'),

            const SizedBox(height: 30),

            const Text(
              'Edit Profile',
              style: TextStyle(
                fontSize: 22,
                fontWeight: FontWeight.bold,
              ),
            ),

            const SizedBox(height: 20),

            Form(
              child: Column(
                children: [
                  const MyTextFormField(name: 'Full Name'),

                  const SizedBox(height: 15),

                  const MyTextFormField(name: 'Email'),

                  const SizedBox(height: 15),

                  const MyTextFormField(name: 'Specialization'),

                  const SizedBox(height: 25),

                  ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.blue,
                      foregroundColor: Colors.white,
                    ),
                    onPressed: () {
                      ScaffoldMessenger.of(context).showSnackBar(
                        const SnackBar(
                          content: Text('Profile form submitted'),
                        ),
                      );
                    },
                    child: const Text('Save Profile'),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}