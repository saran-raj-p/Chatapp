import 'package:flutter/material.dart';
import '../services/httpmethods.dart' as dataService;
import '../services/localstoragemethods.dart' as localStorage;
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
        primaryColor: const Color.fromARGB(255, 94, 95, 94),
        colorScheme: ColorScheme.fromSwatch().copyWith(
          secondary: const Color.fromARGB(255, 94, 95, 94), // darker gray
        ),
      ),
      home: const WhatsAppHomePage(),
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
              onTap: () {
                Navigator.pop(context);
                showCreateContact(context);
              },
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

  void showCreateContact(BuildContext context) {
    final nameController = TextEditingController();
    final emailController = TextEditingController();
    final phoneController = TextEditingController();

    showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: const Text('Create Contact'),
            content: SingleChildScrollView(
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  TextField(
                    controller: nameController,
                    decoration: const InputDecoration(
                        labelText: "Name",
                        hintText: "Enter your Name",
                        prefixIcon: Icon(Icons.person)),
                  ),
                  TextField(
                    controller: emailController,
                    decoration: const InputDecoration(
                        labelText: "Email",
                        hintText: "Enter your Email Id",
                        prefixIcon: Icon(Icons.person)),
                  ),
                  TextField(
                    controller: phoneController,
                    decoration: const InputDecoration(
                        labelText: "Phone Number",
                        hintText: "Enter your Phone Number",
                        prefixIcon: Icon(Icons.person)),
                  ),
                ],
              ),
            ),
            actions: [
              ElevatedButton(
                  onPressed: () => Navigator.of(context).pop(),
                  child: const Text('Cancel')),
              ElevatedButton(
                  onPressed: () async {
                    String name = nameController.text;
                    String email = emailController.text;
                    String phone = phoneController.text;
                    String id = (await localStorage
                        .localstoragemethods()
                        .getlocal('id'))!;
                    Map<String, String> body = {
                      "name": name,
                      "email": email,
                      "phone": phone,
                      "userId": id
                    };

                    // print("Name: $name");
                    // print("Email: $email");
                    // print("Phone: $phone");
                    // print("Phone: $body");

                    try {
                      print("Sending request...");
                      var response = await dataService
                          .httpmethods()
                          .postWithData('Contacts/createContact', body);
                      print("Response received");

                      if (response.statusCode == 200 &&
                          response.data["message"] ==
                              "Contact created successfully") {
                        ScaffoldMessenger.of(context).showSnackBar(
                          const SnackBar(
                              content: Text("Contact created successfully")),
                        );
                        Navigator.of(context).pop();
                      } else {
                        print('no');
                        ScaffoldMessenger.of(context).showSnackBar(
                          SnackBar(
                              content: Text(
                                  "Failed: ${response.data["details"] ?? "Unknown error"}")),
                        );
                      }
                    } catch (e) {
                      print('❌ Error occurred: $e');
                    }
                  },
                  child: const Text('Create Contact'))
            ],
          );
        });
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
            indicatorColor: Colors.white, // underline color
            indicatorWeight: 3.0, // underline thickness
            labelColor: Colors.white, // selected tab color
            unselectedLabelColor: Colors.grey[300], // unselected tab color
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
        body: const TabBarView(
          children: [
            Center(child: Text("Camera")),
            ChatsTab(),
            Center(child: Text("Status")),
            Center(child: Text("Calls")),
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

class ChatsTab extends StatefulWidget {
  const ChatsTab({super.key});

  @override
  State<ChatsTab> createState() => _ChatsTabState();
}

class _ChatsTabState extends State<ChatsTab> {
  List<dynamic> contacts = [];
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    fetchContacts();
  }

  Future<void> fetchContacts() async {
    try {
      String userId =
          (await localStorage.localstoragemethods().getlocal('id'))!;
      final response = await dataService
          .httpmethods()
          .getData("Contacts/getContact?userId=$userId");

      setState(() {
        contacts = response["data"];
        isLoading = false;
      });
      print(contacts);
      print('ok');
    } catch (e) {
      print("❌ Error loading contacts: $e");
      setState(() {
        isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    if (isLoading) {
      return const Center(child: CircularProgressIndicator());
    }

    if (contacts.isEmpty) {
      return const Center(child: Text("No contacts found"));
    }

    return ListView.builder(
      itemCount: contacts.length,
      itemBuilder: (context, index) {
        final contact = contacts[index];
        return ListTile(
          leading: CircleAvatar(
            backgroundColor: Colors.blueAccent,
            child: Text(contact['name'][0].toUpperCase()),
          ),
          title: Text(contact['name']),
        );
      },
    );
  }
}
