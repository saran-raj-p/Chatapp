import 'package:flutter/material.dart';

class Login extends StatelessWidget {
  const Login({super.key});

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
      body: Container(
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              const Text(
                "Login",
                selectionColor: Colors.cyan,
              ),
              Padding(
                padding: EdgeInsets.symmetric(horizontal: 100),
                child: TextFormField(
                  decoration: const InputDecoration(
                      hintText: "Enter Username",
                      labelText: "UserName",
                      border: OutlineInputBorder()),
                ),
              ),
              Padding(
                padding: EdgeInsets.symmetric(horizontal: 100),
                child: TextFormField(
                  decoration: const InputDecoration(
                      hintText: "Enter Password",
                      labelText: "Password",
                      border: OutlineInputBorder()),
                ),
              ),
              TextButton(
                onPressed: null,
                style: TextButton.styleFrom(
                    backgroundColor: Colors.red,
                    fixedSize: const Size(100, 10),
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10))),
                child: const Text("Login"),
              )
            ],
          ),
        ),
      ),
    ));
  }
}
