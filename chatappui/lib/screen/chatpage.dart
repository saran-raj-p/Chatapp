import 'package:flutter/material.dart';

// void main() {
//   runApp(WhatsAppClone());
// }

class WhatsAppClone extends StatelessWidget {
  const WhatsAppClone({super.key});

 @override
Widget build(BuildContext context) { 
  return MaterialApp(
    title: 'WhatsApp UI',
    theme: ThemeData(
      primaryColor: const Color.fromARGB(255,94,95,94),
      colorScheme: ColorScheme.fromSwatch().copyWith(
        secondary: const Color.fromARGB(255, 94, 95, 94), // darker gray
      ),
    ),
    home: WhatsAppHomePage(),
  );
}
}

class WhatsAppHomePage extends StatefulWidget {
  const WhatsAppHomePage({super.key});

  @override
  State<WhatsAppHomePage> createState() => _WhatsAppHomePageState();
}

class _WhatsAppHomePageState extends State<WhatsAppHomePage> {
  int _currentTab = 1;

  void _showChatOptions(BuildContext context) {
    showModalBottomSheet(
      context: context,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.vertical(top: Radius.circular(16)),
      ),
      builder: (_) {
        return Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            ListTile(
              leading: const Icon(Icons.group),
              title: const Text('New Group'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: const Icon(Icons.person_add),
              title: const Text('New Contact'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: const Icon(Icons.groups),
              title: const Text('New Community'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: const Icon(Icons.contacts),
              title: const Text('Contacts on WhatsApp'),
              onTap: () => Navigator.pop(context),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 4,
      initialIndex: 1,
      child: Scaffold(
        appBar: AppBar(
          title: const Text('WhatsApp'),
          backgroundColor: const Color.fromARGB(255, 130, 129, 129),
          actions: [
            const Icon(Icons.camera_alt),
            const SizedBox(width: 16),
            const Icon(Icons.search),
            const SizedBox(width: 16),
            PopupMenuButton<String>(
              itemBuilder: (BuildContext context) {
                return ['New group', 'Settings', 'Logout']
                    .map((String choice) => PopupMenuItem<String>(
                          value: choice,
                          child: Text(choice),
                        ))
                    .toList();
              },
            ),
          ],
        bottom: TabBar(
           indicatorColor: Colors.white,              // underline color
           indicatorWeight: 3.0,                      // underline thickness
           labelColor: Colors.white,                  // selected tab color
           unselectedLabelColor: Colors.grey[300],    // unselected tab color
        onTap: (index) {
          setState(() {
            _currentTab = index;
          });
        },
      tabs: const [
        Tab(icon: Icon(Icons.camera_alt)),
        Tab(text: "CHATS"),
        Tab(text: "STATUS"),
        Tab(text: "CALLS"),
      ],
    ),
        ),
        body: TabBarView(
          children: [
            const Center(child: Text("Camera")),
            ChatsTab(),
            const Center(child: Text("Status")),
            const Center(child: Text("Calls")),
          ],
        ),
        floatingActionButton: _currentTab == 1
            ? FloatingActionButton(
                backgroundColor: const Color.fromARGB(255, 6, 42, 245),
                onPressed: () => _showChatOptions(context),
                child: const Icon(Icons.add, color: Colors.white),
              )
            : null,
      ),
    );
  }
}

class ChatsTab extends StatelessWidget {
  final List<Map<String, String>> chats = [
    {"name": "Alice", "message": "Hey!", "time": "09:00"},
    {"name": "Bob", "message": "What's up?", "time": "10:15"},
    {"name": "Charlie", "message": "Let's meet.", "time": "12:30"},
  ];

  ChatsTab({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: chats.length,
      itemBuilder: (context, index) {
        return ListTile(
          leading: CircleAvatar(
            backgroundColor: const Color.fromARGB(255, 243, 4, 4),
            child: Text(chats[index]['name']![0]),
          ),
          title: Text(chats[index]['name']!),
          subtitle: Text(chats[index]['message']!),
          trailing: Text(chats[index]['time']!),
        );
      },
    );
  }
}
