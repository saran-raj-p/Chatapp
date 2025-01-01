class ProfileModel {
  final String id;
  final String name;
  final String email;
  final String phone;
  final String password;

  ProfileModel({
    required this.id,
    required this.name,
    required this.email,
    required this.phone,
    required this.password,
  });

  factory ProfileModel.fromJson(Map<String, dynamic> json) {
    return ProfileModel(
      id: json['id'] ?? '',
      name: json['name'] ?? '',
      email: json['email'] ?? '',
      phone: json['phone'] ?? '',
      password: json['password'] ?? '',
    );
  }
}
