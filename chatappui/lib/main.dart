import 'package:chatappui/forgotpassword/forgotpassword.dart';
import 'package:chatappui/profile/Profile.dart';
import 'package:flutter/material.dart';
import 'register/register.dart';

void main() {
  runApp(MaterialApp(
    initialRoute: '/',
    routes: {
      '/': (context) => const MainApp(),
      '/register': (context) => const Register(),
      '/profile': (context) => const Profile(),
      '/forgot': (context) => const Forgotpassword(),
    },
  ));
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        body: Column(
          children: [
            TextButton(
              onPressed: () {
                Navigator.pushNamed(context, '/register');
              },
              child: const Text(
                "Register",
                selectionColor: Colors.cyan,
              ),
            ),
            TextButton(
              onPressed: () {
                Navigator.pushNamed(context, '/forgot');
              },
              child: const Text(
                "ForgotPassword",
                selectionColor: Colors.cyan,
              ),
            ),
            TextButton(
              onPressed: () {
                Navigator.pushNamed(context, '/profile');
              },
              child: const Text(
                "Profile",
                selectionColor: Colors.cyan,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
