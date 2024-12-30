import 'dart:convert';

import 'package:chatappui/services/httpmethods.dart' as data;
import 'package:chatappui/services/localstoragemethods.dart';
import 'package:flutter/material.dart';
import 'package:chatappui/screen/forgotpassword.dart';

class Login extends StatelessWidget {
  Login({super.key});
  final userNameController = new TextEditingController();
  final userPasswordController = new TextEditingController();
  final localstoragemethods local = new localstoragemethods();
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
                  controller: userNameController,
                  decoration: const InputDecoration(
                      hintText: "Enter Username",
                      labelText: "UserName",
                      border: OutlineInputBorder()),
                ),
              ),
              Padding(
                padding: EdgeInsets.symmetric(horizontal: 100),
                child: TextFormField(
                  controller: userPasswordController,
                  decoration: const InputDecoration(
                      hintText: "Enter Password",
                      labelText: "Password",
                      border: OutlineInputBorder()),
                ),
              ),
              GestureDetector(
                onTap: () {
                  // Navigate to ForgotPassword page
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => Forgotpassword()),
                  );
                },
                child: Text(
                  'Forgot Password?',
                  style: TextStyle(
                    decoration: TextDecoration.underline,
                    color: Theme.of(context).colorScheme.secondary,
                    fontSize: 20,
                  ),
                ),
              ),
              TextButton(
                onPressed: () async {
                  Map<String, dynamic> body = {
                    "Email": userNameController.text,
                    "Password": userPasswordController.text,
                  };
                  var response = await data
                      .httpmethods()
                      .postWithData('Auth/UserLogin/', body);
                  if (response.statusCode == 200) {
                    var resource = jsonDecode(response.body);
                    local.setLocal('Access', resource['accessToken']);
                    local.setLocal('Refresh', resource['refreshToken']);
                    ScaffoldMessenger.of(context).showSnackBar(const SnackBar(
                      content: Text('Login Successful'),
                      duration: Durations.extralong4,
                    ));
                    Navigator.pushNamed(context, '/profile');
                  } else {
                    ScaffoldMessenger.of(context).showSnackBar(const SnackBar(
                        content: Text('Login Failed Invalid Credentials')));
                  }
                },
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
