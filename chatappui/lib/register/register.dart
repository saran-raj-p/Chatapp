import 'package:flutter/material.dart';

class Register extends StatelessWidget {
  const Register({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        child: Container(
          padding: const EdgeInsets.all(16.0), // Add padding around the container
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
                    decoration: const InputDecoration(
                      hintText: 'Enter your name',
                      labelText: 'Your Name',
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(20)),
                        borderSide: BorderSide(width: 1.0), // Reduced border width
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
                    decoration: const InputDecoration(
                      hintText: 'Enter your email',
                      labelText: 'Your Email',
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(20)),
                        borderSide: BorderSide(width: 1.0), // Reduced border width
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
                    decoration: const InputDecoration(
                      hintText: 'Enter your password',
                      labelText: 'Your Password',
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(20)),
                        borderSide: BorderSide(width: 1.0), // Reduced border width
                      ),
                    ),
                    obscureText: true, // Optional: hide password input
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
