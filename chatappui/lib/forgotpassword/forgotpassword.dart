import 'package:flutter/material.dart';

voidmain() => runApp(MaterialApp(home: Forgotpassword()));

class Forgotpassword extends StatelessWidget {
  const Forgotpassword({super.key});

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
            width: 300,
            height: 60,
            child: Center(
                child: TextField(
              decoration: InputDecoration(
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(10.0),
                ),
                filled: true,
                hintStyle: TextStyle(color: Colors.grey[800]),
                hintText: "Enter new Password",
                fillColor: Colors.white70,
              ),
            ))),
        Container(
          width: 300, // Set your desired width for the Container
          height: 60, // Set your desired height for the Container
          child: Center(
            child: TextField(
              decoration: InputDecoration(
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(10.0),
                ),
                filled: true,
                hintStyle: TextStyle(color: Colors.grey[800]),
                hintText: "Enter COnfirm password",
                fillColor: Colors.white70,
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
