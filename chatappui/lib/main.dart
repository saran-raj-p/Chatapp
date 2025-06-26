import 'package:chatappui/screen/Profile.dart';
import 'package:chatappui/screen/chatpage.dart';
import 'package:chatappui/screen/edit_profile.dart';
import 'package:chatappui/screen/get_started.dart';
import 'package:chatappui/screen/login.dart';
import 'package:chatappui/screen/register.dart';
import 'package:chatappui/screen/forgotpassword.dart';
import 'package:flutter/material.dart';

void main() {
  runApp(MaterialApp(
    debugShowCheckedModeBanner: false,
    initialRoute: '/Main',
    routes: {
      '/Main': (context) => const MainApp(),
      '/login': (context) => Login(),
      '/register': (context) => Register(),
      '/profile': (context) => Profile(),
      '/getstarted': (context) => const GetStarted(),
      '/forgot': (context) => Forgotpassword(),
      '/editProfile': (context) => const EditProfile(),
      '/chatpage':(context) => const WhatsAppHomePage(),
      '/start': (context) => GetStarted(),
    },
  ));
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
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
              Navigator.pushNamed(context, '/profile');
            },
            child: const Text(
              "Profile",
              selectionColor: Colors.cyan,
            ),
          ),
          TextButton(
            onPressed: () {
              Navigator.pushNamed(context, '/getstarted');
            },
            child: const Text(
              "Get Started",
              selectionColor: Colors.cyan,
            ),
          ),
          TextButton(
            onPressed: () {
              Navigator.pushNamed(context, '/forgot');
            },
            child: const Text(
              "Forgotpassword",
              selectionColor: Colors.cyan,
            ),
          ),

          TextButton(
            onPressed: () {
              Navigator.pushNamed(context, '/chatpage');
            },
            child: const Text(
              "ChatPage",
              selectionColor: Colors.cyan,
            ),
          ),
          TextButton(
              onPressed: () {
                Navigator.pushNamed(context, '/login');
              },
              child: const Text(
                "Login",
                selectionColor: Colors.cyan,
              )),
          TextButton(
              onPressed: () {
                Navigator.pushNamed(context, '/editProfile');
              },
              child: const Text(
                "Edit Profile",
                selectionColor: Colors.cyan,
              )),
          TextButton(
              onPressed: () {
                Navigator.pushNamed(context, '/start');
              },
              child: const Text(
                "Start",
                selectionColor: Colors.cyan,
              ))
        ],
      ),
    );
  }
}
