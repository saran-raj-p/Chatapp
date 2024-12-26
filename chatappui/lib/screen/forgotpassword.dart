//import 'package:flutter/material.dart';

//class Forgotpassword extends StatelessWidget {
//const Forgotpassword({super.key});

import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

class Forgotpassword extends StatelessWidget {
  final TextEditingController newPasswordController = TextEditingController();
  final TextEditingController confirmPasswordController =
      TextEditingController();

  Future<void> resetPassword() async {
    final url = Uri.parse('http://your-api-url/api/auth/reset-password');
    final response = await http.post(
      url,
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({
        'newPassword': newPasswordController.text,
        'confirmPassword': confirmPasswordController.text,
        'resetToken': 'sample-reset-token', // Replace with actual token
      }),
    );

    if (response.statusCode == 200) {
      print('Password reset successful');
    } else {
      print('Failed: ${response.body}');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
      children: [
        Container(
          width: 300,
          child: Image.asset('images/forgot.png'),
        ),
        Container(
          child: Text(
            'Lets chat with your friends',
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
        ),
        Container(
          alignment: FractionalOffset(0.2, 0.6),
          child: Text(
            'ForgotPassword',
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
        ),
        Container(
          child: SizedBox(
            width: 300, // Set the desired width
            child: TextFormField(
              decoration: const InputDecoration(
                hintText: 'Enter your Registered Mail_id',
                labelText: 'Mail_id',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.all(Radius.circular(20)),
                  borderSide: BorderSide(width: 1.0), // Reduced border width
                ),
              ),
            ),
          ),
        ),
        Container(
          child: SizedBox(
            width: 300, // Set the desired width
            child: TextFormField(
              decoration: const InputDecoration(
                hintText: 'Enter new Password',
                labelText: 'New password',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.all(Radius.circular(20)),
                  borderSide: BorderSide(width: 1.0), // Reduced border width
                ),
              ),
            ),
          ),
        ),
        Container(
          child: SizedBox(
            width: 300, // Set the desired width
            child: TextFormField(
              decoration: const InputDecoration(
                hintText: 'Enter Confirm Password',
                labelText: 'Confirm password',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.all(Radius.circular(20)),
                  borderSide: BorderSide(width: 1.0), // Reduced border width
                ),
              ),
            ),
          ),
        ),
        Container(
          child: TextButton(
            style: TextButton.styleFrom(
              foregroundColor: Colors.blue, // Use this for the text color
              backgroundColor: Colors.red,
            ),
            onPressed: null,
            child: Text('submit'),
          ),
        )
      ],
    ));
  }
}
