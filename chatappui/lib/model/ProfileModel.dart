class ProfileModel {
  final String id;
  final String name;
  final String email;
  final String phone;
  final String password;
  final String profileUrl;

  ProfileModel(
      {required this.id,
      required this.name,
      required this.email,
      required this.phone,
      required this.password,
      required this.profileUrl});

  factory ProfileModel.fromJson(Map<String, dynamic> json) {
    return ProfileModel(
        id: json['id'] ?? '',
        name: json['name'] ?? 'Loading...',
        email: json['email'] ?? '',
        phone: json['phone'] ?? '',
        password: json['password'] ?? '',
        profileUrl: json['profileUrl'] ?? '');
  }
}
