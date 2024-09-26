import 'package:flutter/material.dart';

void main() {
  runApp(const Profile());
}

class Profile extends StatelessWidget {
  const Profile({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(
          title: const Text('My Profile'),
          backgroundColor: Colors.cyan,
        ),
        body: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            const Text(
              'Hello, New User',
              style: TextStyle(
                fontSize: 30,
              ),
            ),
            SizedBox(
              height: 90,
              child: Stack(
                children: [
                  const Positioned(
                      top: 0,
                      child: CircleAvatar(
                        backgroundImage: AssetImage('images/profile.jpg'),
                        radius: 40,
                      )),
                  Positioned(
                      right: 0,
                      top: 30,
                      child: ElevatedButton.icon(
                        onPressed: null,
                        icon: const Icon(
                          Icons.edit,
                          color: Colors.blue,
                        ),
                        style: ElevatedButton.styleFrom(
                          foregroundColor: Colors.red,
                        ),
                        label: const Text(
                          "Edit Profile",
                          style: TextStyle(
                            color: Colors.black,
                          ),
                        ),
                      ))
                ],
              ),
            ),
            const Text('New User'),
            const Text('abc123@gmail.com'),
            const Divider(
              height: 5,
              color: Colors.black,
            )
          ],
        ),
      ),
    );
  }
}
