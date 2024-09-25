
import 'package:flutter/material.dart';
import 'register/register.dart';
void main() {
  runApp(MaterialApp(
    initialRoute: '/',
    routes: {
      '/': (context) => const MainApp(),
      '/register': (context) => const Register(),
    },
  ));
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        body: Center(
          child: TextButton(
            onPressed: () {
              Navigator.pushNamed(context, '/register');
            },
            child: const Text(
              "Register",
              selectionColor: Colors.cyan,
            ),
          ),
        ),
      ),
    );
  }
}
