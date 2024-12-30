import 'dart:typed_data';
import 'package:chatappui/model/ProfileModel.dart';
import 'package:flutter/material.dart';
import 'package:http_parser/http_parser.dart';
import 'package:image_picker/image_picker.dart';
import '../services/httpmethods.dart' as dataService;
import 'package:dio/dio.dart';
import 'package:mime/mime.dart';

class EditProfile extends StatefulWidget {
  EditProfile({super.key});

  @override
  State<EditProfile> createState() => _EditProfile();
}

class _EditProfile extends State<EditProfile> {
  Uint8List? _profileImage; // For displaying the image
  XFile? _pickedFile; // To store the selected file (web-compatible)

  final ImagePicker imagePicker = ImagePicker();

  // Controllers for input fields
  final usernameController = TextEditingController();
  final emailController = TextEditingController();
  final phoneNumberController = TextEditingController();

  // Variable to store the profile data
  ProfileModel? profile;
  String? profileUrl;

  // Function to pick an image from the gallery
  Future<void> pickFromGallery() async {
    final XFile? pickedFile =
        await imagePicker.pickImage(source: ImageSource.gallery);
    if (pickedFile != null) {
      final bytes = await pickedFile.readAsBytes();
      setState(() {
        _profileImage = bytes;
        _pickedFile = pickedFile;
      });
    }
  }

  // Function to capture an image from the camera
  Future<void> takePhoto() async {
    final XFile? pickedFile =
        await imagePicker.pickImage(source: ImageSource.camera);
    if (pickedFile != null) {
      final bytes = await pickedFile.readAsBytes();
      setState(() {
        _profileImage = bytes;
        _pickedFile = pickedFile;
      });
    }
  }

  // Show a dialog with options to select or capture an image
  void showProfileDialog() {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        content: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            ListTile(
              leading: Icon(Icons.photo, color: Colors.black),
              title: Text('Select photo from gallery'),
              onTap: () {
                Navigator.of(context).pop();
                pickFromGallery();
              },
            ),
            Divider(),
            ListTile(
              leading: Icon(Icons.camera_alt, color: Colors.black),
              title: Text('Take photo with camera'),
              onTap: () {
                Navigator.of(context).pop();
                takePhoto();
              },
            ),
          ],
        ),
      ),
    );
  }

  // Function to upload profile data to the backend
  Future<void> uploadProfileData() async {
    try {
      // Prepare form data for the request
      final formData = FormData.fromMap({
        'Name': usernameController.text,
        'Email': emailController.text,
        'Phone': phoneNumberController.text,
        'Id':
            '014A619A-9655-40BD-BB6F-55C2295022A5', // Replace with actual user ID
        if (_pickedFile != null)
          'Image': await MultipartFile.fromFile(
            _pickedFile!.path,
            filename: _pickedFile!.name,
            contentType: MediaType.parse(
                lookupMimeType(_pickedFile!.path) ?? 'image/jpeg'),
          ),
      });

      // Use httpmethods to send the request
      final response = await dataService.httpmethods().postWithData(
            'Profile/updateProfile',
            formData,
          );

      if (response.statusCode == 200) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Profile updated successfully')),
        );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
              content:
                  Text('Failed to update profile: ${response.statusCode}')),
        );
      }
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error uploading profile: $e')),
      );
    }
  }

  // Fetch profile data from the backend
  void getProfile() async {
    String id =
        "21919F62-3C39-434C-A63E-3AEE1F59CD7C"; // Replace with actual user ID

    try {
      final response =
          await dataService.httpmethods().getData('Profile/getprofile?id=$id');

      if (response.containsKey('data')) {
        final Map<String, dynamic> data = response;

        setState(() {
          profile = ProfileModel.fromJson(data['data']);
          usernameController.text = profile!.name;
          emailController.text = profile!.email;
          phoneNumberController.text = profile!.phone;
          profileUrl = profile!.profileUrl;
        });
      } else {
        print('Failed to load profile data.');
      }
    } catch (e) {
      print('Error occurred: $e');
    }
  }

  @override
  void initState() {
    super.initState();
    getProfile();
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(
          title: Text('Edit Profile'),
        ),
        body: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              GestureDetector(
                onTap: showProfileDialog,
                child: CircleAvatar(
                  radius: 60,
                  backgroundImage: _profileImage != null
                      ? MemoryImage(_profileImage!)
                      : (profileUrl != null && profileUrl!.isNotEmpty)
                          ? NetworkImage(profileUrl!)
                          : const AssetImage('images/profiles.png')
                              as ImageProvider<Object>,
                ),
              ),
              const SizedBox(height: 10),
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: SizedBox(
                  width: 350,
                  child: TextFormField(
                    controller: usernameController,
                    decoration: const InputDecoration(
                      hintText: "Enter Your Name",
                      labelText: "Username",
                      prefixIcon: Icon(Icons.person_4_rounded),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(20)),
                        borderSide: BorderSide(width: 1.0),
                      ),
                    ),
                  ),
                ),
              ),
              const SizedBox(height: 10),
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: SizedBox(
                  width: 350,
                  child: TextFormField(
                    controller: emailController,
                    decoration: const InputDecoration(
                      hintText: "Enter Your Email",
                      labelText: "Email",
                      prefixIcon: Icon(Icons.email),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(20)),
                        borderSide: BorderSide(width: 1.0),
                      ),
                    ),
                  ),
                ),
              ),
              const SizedBox(height: 10),
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: SizedBox(
                  width: 350,
                  child: TextFormField(
                    controller: phoneNumberController,
                    decoration: const InputDecoration(
                      hintText: "Enter Your Phone Number",
                      labelText: "Phone Number",
                      prefixIcon: Icon(Icons.phone),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(20)),
                        borderSide: BorderSide(width: 1.0),
                      ),
                    ),
                  ),
                ),
              ),
              const SizedBox(height: 200),
              SizedBox(
                width: 350,
                height: 50,
                child: TextButton(
                  onPressed: uploadProfileData,
                  style: TextButton.styleFrom(
                    backgroundColor: Colors.black,
                    foregroundColor: Colors.white,
                  ),
                  child: const Text('SAVE'),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
