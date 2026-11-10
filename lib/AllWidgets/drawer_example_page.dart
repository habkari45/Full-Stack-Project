import 'package:flutter/material.dart';
import '../home_page.dart';
import '../profile_page.dart';

class DrawerExamplePage extends StatelessWidget {
  const DrawerExamplePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: Drawer(
        child: Column(
          children: [
            // الهيدر العلوي
            const UserAccountsDrawerHeader(
              accountName: Text('Sultan Al-Habkari'),
              accountEmail: Text('Sultan Portfolio'),
              currentAccountPicture: CircleAvatar(
                backgroundColor: Colors.white,
                child: Text(
                  'S',
                  style: TextStyle(
                    fontSize: 24,
                    color: Colors.blue,
                  ),
                ),
              ),
              decoration: BoxDecoration(
                color: Colors.blue,
              ),
            ),

            // عناصر القائمة
            ListTile(
              leading: const Icon(Icons.home),
              title: const Text('Home'),
              subtitle: const Text('Go to Home screen'),
              trailing: Icon(Icons.arrow_forward_ios),
              onTap: () {
                Navigator.pop(context);

                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const HomePage(),
                  ),
                );
              },
            ),

            ListTile(
              leading: const Icon(Icons.person),
              title: const Text('Profile'),
              onTap: () {
                Navigator.pop(context);

                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const ProfilePage(),
                  ),
                );
              },
            ),

            const Divider(),

            ListTile(
              leading: const Icon(Icons.settings),
              title: const Text('Settings'),
              onTap: () {
                Navigator.pop(context);
                print('Settings tapped');
              },
            ),

            const Spacer(),

            // زر تسجيل الخروج في الأسفل
            ListTile(
              leading: const Icon(Icons.logout, color: Colors.red),
              title: const Text(
                'Logout',
                style: TextStyle(color: Colors.red),
              ),
              onTap: () {
                Navigator.pop(context);
                print('Logout tapped');
              },
            ),
          ],
        ),
      ),

      // القائمة الجانبية الثانية
      endDrawer: Drawer(
        child: SafeArea(
          child: Column(
            children: [
              const ListTile(
                title: Text(
                  'القائمة الجانبية (يمين)',
                  style: TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 18,
                  ),
                ),
              ),
              const Divider(),
              ListTile(
                leading: const Icon(Icons.info),
                title: const Text('عن التطبيق'),
                onTap: () {
                  Navigator.pop(context);
                  print('About tapped');
                },
              ),
            ],
          ),
        ),
      ),

      appBar: AppBar(
        title: const Text('Drawer Demo'),
        backgroundColor: Colors.blue,
        actions: [
          Builder(
            builder: (context) => IconButton(
              icon: const Icon(Icons.menu_open),
              onPressed: () {
                Scaffold.of(context).openEndDrawer();
              },
            ),
          ),
        ],
      ),

      body: const Center(
        child: Text(
          'اسحب من اليسار لفتح الـ Drawer\n'
              'واسحب من اليمين لفتح الـ endDrawer',
          textAlign: TextAlign.center,
          style: TextStyle(fontSize: 18),
        ),
      ),
    );
  }
}