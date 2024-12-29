import 'package:shared_preferences/shared_preferences.dart';

class localstoragemethods {
  Future<bool> setLocal(String Key, dynamic Value) async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    if (Value is String) {
      return pref.setString(Key, Value);
    } else if (Value is int) {
      return pref.setInt(Key, Value);
    } else if (Value is double) {
      return pref.setDouble(Key, Value);
    } else if (Value is bool) {
      return pref.setBool(Key, Value);
    } else if (Value is List<String>) {
      return pref.setStringList(Key, Value);
    } else {
      throw Exception("Unsupported value type");
    }
  }

  Future<dynamic> getlocal(String Key) async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    if (pref.containsKey(Key)) {
      var value = pref.get(Key);
      return value;
    } else {
      return null;
    }
  }
}
