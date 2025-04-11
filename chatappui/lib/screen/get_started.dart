import 'package:flutter/material.dart';

class GetStarted extends StatelessWidget {
  const GetStarted({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Image.asset(
              'images/get started.png',
              width: 300,
              height: 300,
            ),
            const SizedBox(height: 40),
            const Text(
              'Lets Chat with your Friends',
              style: TextStyle(
                fontSize: 20,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(
              height: 50,
            ),
            const Text('A chat application makes it easy'),
            const Text('to communicate with people anywhere in the'),
            const Text('world by sending and receiving messages.'),
            const SizedBox(height: 110),
            TextButton(
              onPressed: () {
                Navigator.pushNamed(context, '/register');
              },
              style: TextButton.styleFrom(
                backgroundColor: Colors.black,
                foregroundColor: Colors.white,
                shape: const RoundedRectangleBorder(
                  borderRadius:
                      BorderRadius.zero, // Set to zero for square corners
                ),
              ),
              child: Container(
                width: 255,
                height: 39,
                child: const Center(
                  child: Text(
                    'Get Started',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ),
            ),
            //const SizedBox(height: 100)
          ],
        ),
      ),
    );
  }
}
