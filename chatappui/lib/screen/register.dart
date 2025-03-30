import 'dart:html';
import 'package:chatappui/services/httpmethods.dart' as data;
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class Register extends StatelessWidget {
  Register({super.key});
  final nameController = new TextEditingController();
  final emailController = new TextEditingController();
  final passwordController = new TextEditingController();
  final phoneController = new TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        child: Container(
          padding:
              const EdgeInsets.all(16.0), // Add padding around the container
          child: Column(
            children: [
              Image.asset(
                'images/login.png',
                fit: BoxFit.cover,
                height: 300,
                width: 300,
              ),
              const SizedBox(height: 20),
              Center(
                child: SizedBox(
                  width: 300, // Set the desired width
                  child: TextFormField(
                    controller: nameController,
                    decoration: const InputDecoration(
                      hintText: 'Enter your name',
                      labelText: 'Your Name',
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(20)),
                        borderSide:
                            BorderSide(width: 1.0), // Reduced border width
                      ),
                    ),
                  ),
                ),
              ),
              const SizedBox(height: 20),
              Center(
                child: SizedBox(
                  width: 300, // Set the desired width
                  child: TextFormField(
                    controller: emailController,
                    decoration: const InputDecoration(
                      hintText: 'Enter your email',
                      labelText: 'Your Email',
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(20)),
                        borderSide:
                            BorderSide(width: 1.0), // Reduced border width
                      ),
                    ),
                  ),
                ),
              ),
              const SizedBox(height: 20),
              Center(
                child: SizedBox(
                  width: 300, // Set the desired width
                  child: TextFormField(
                    controller: phoneController,
                    decoration: const InputDecoration(
                      hintText: 'Enter your phone number',
                      labelText: 'Phone Number',
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(20)),
                        borderSide:
                            BorderSide(width: 1.0), // Reduced border width
                      ),
                    ),
                    obscureText: false, // Optional: hide password input
                  ),
                ),
              ),
              const SizedBox(height: 20),
              Center(
                child: SizedBox(
                  width: 300, // Set the desired width
                  child: TextFormField(
                    controller: passwordController,
                    decoration: const InputDecoration(
                      hintText: 'Enter your password',
                      labelText: 'Your Password',
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(20)),
                        borderSide:
                            BorderSide(width: 1.0), // Reduced border width
                      ),
                    ),
                    obscureText: true, // Optional: hide password input
                  ),
                ),
              ),
              const SizedBox(height: 20),
              Center(
                child: SizedBox(
                  width: 300, // Set the desired width
                  child: TextButton(
                      onPressed: () async {
                        //print("clicked");
                        try {
                          Map<String, dynamic> body = {
                            "name": nameController.text,
                            "email": emailController.text,
                            "phone": phoneController.text,
                            "password": passwordController.text,
                          };
                          bool allValuesAreEmpty = body.values.any((value) =>
                              value == null || value.toString().trim().isEmpty);
                          if (allValuesAreEmpty) {
                            ScaffoldMessenger.of(context)
                                .showSnackBar(const SnackBar(
                              content: Text("Please Enter Valid Input"),
                              duration: Duration(seconds: 5),
                            ));
                          } else {
                            var response = await data
                                .httpmethods()
                                .postWithData('Auth/UserRegistration/', body);
                            if (response.statusCode == 200) {
                              ScaffoldMessenger.of(context)
                                  .showSnackBar(const SnackBar(
                                content: Text("Registeration Successful"),
                                duration: Duration(seconds: 5),
                              ));
                              Navigator.pushNamed(context, '/login');
                            } else {
                              ScaffoldMessenger.of(context)
                                  .showSnackBar(const SnackBar(
                                content: Text("Registeration Failed"),
                                duration: Duration(seconds: 5),
                              ));
                            }
                          }
                        } catch (e) {
                          ScaffoldMessenger.of(context)
                              .showSnackBar(const SnackBar(
                            content: Text("Error Occured"),
                            duration: Duration(seconds: 5),
                          ));
                        }
                      },
                      style: TextButton.styleFrom(
                          backgroundColor: Colors.red,
                          fixedSize: const Size(100, 10),
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(10))),
                      child: const Text("Register")),
                ),
              ),
              GestureDetector(
                onTap: () {
                  // Navigate to Login page
                  Navigator.pushNamed(context, '/login');
                },
                child: RichText(
                  text: const TextSpan(
                    style: TextStyle(color: Colors.grey),
                    children: [
                      TextSpan(text: 'Already have an account? '),
                      TextSpan(
                        text: 'Login Here...',
                        style: TextStyle(
                          color: Colors.blue,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
