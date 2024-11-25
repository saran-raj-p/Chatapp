import 'package:flutter/material.dart';

class Profile extends StatelessWidget {
  Profile({super.key});

  final List<Friend> friends = [
    Friend(
      name: 'Saran',
      image: Image.asset('images/profiles.png'), // Local image asset
    ),
    Friend(
      name: 'Jagan',
      image: Image.asset('images/profiles.png'),
    ),
    Friend(
      name: 'Lv',
      image: Image.asset('images/profiles.png'),
    ),
    Friend(
      name: 'Yuva',
      image: Image.asset('images/profiles.png'),
    ),
    // Add more friends here
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          const SizedBox(height: 30),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              const Text(
                'Hello, New User',
                style: TextStyle(
                  fontSize: 20,
                ),
              ),
              Row(
                children: [
                  const CircleAvatar(
                    foregroundImage: AssetImage('images/profiles.png'),
                  ),
                  PopupMenuButton(
                    itemBuilder: (context) => [
                      const PopupMenuItem(
                          value: 1,
                          child: Row(
                            children: [Icon(Icons.home), Text("Home")],
                          )),
                      const PopupMenuItem(
                          value: 2,
                          child: Row(
                            children: [Icon(Icons.home), Text("Home")],
                          )),
                      const PopupMenuItem(
                          value: 3,
                          child: Row(
                            children: [Icon(Icons.home), Text("Home")],
                          )),
                      const PopupMenuItem(
                          value: 4,
                          child: Row(
                            children: [Icon(Icons.home), Text("Home")],
                          ))
                    ],
                  ),
                ],
              )
            ],
          ),
          const SizedBox(height: 60),
          Container(
            alignment: Alignment.center,
            child: Column(
              children: [
                TextButton(
                  onPressed: () {
                    //Navigator.pushNamed(context, '');
                  },
                  style: TextButton.styleFrom(
                    backgroundColor: Colors.black,
                    foregroundColor: Colors.white,
                    // shape: const RoundedRectangleBorder(
                    //   borderRadius:
                    //       BorderRadius.zero, // Set to zero for square corners
                    // ),
                  ),
                  child: Container(
                    padding:
                        const EdgeInsets.symmetric(vertical: 4, horizontal: 20),
                    child: const Text(
                      'Find Nearby Friends >',
                    ),
                  ),
                ),
              ],
            ),
          ),
          const SizedBox(height: 60),
          const Text('Chat With Friends'),
          Container(
              height: 200,
              child: Card(
                color: Colors.black,
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: ListView.builder(
                    itemCount: friends.length,
                    itemBuilder: (context, index) {
                      return Card(
                        child: ListTile(
                          onTap: () {},
                          title: Text(friends[index].name),
                          leading: CircleAvatar(
                            foregroundImage: friends[index].image.image,
                          ),
                        ),
                      );
                    },
                  ),
                ),
              )),
        ],
      ),
    );
  }
}

class Friend {
  final String name;
  final Image image;

  Friend({required this.name, required this.image});
}
