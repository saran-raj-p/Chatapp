import 'dart:convert';

import 'package:http/http.dart' as http;

class httpmethods {
  Future<http.Response> postWithData(String url, dynamic bodyData) async {
    try {
      // Ensure the body is JSON-encoded
      final encodedBody = jsonEncode(bodyData);
      String uri = "https://localhost:7165/api/$url";
      final response = await http.post(
        Uri.parse(uri),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: encodedBody,
      );
      switch (response.statusCode) {
        case 200:
          return response;
        case 401:
          return http.Response(
            jsonEncode({'message': 'Unauthorized', 'details': response.body}),
            401,
          );
        default:
          return http.Response(
            jsonEncode({
              'message': 'Unexpected error',
              'statusCode': response.statusCode,
              'details': response.body,
            }),
            response.statusCode,
          );
      }
    } catch (e) {
      // Handle errors and return a fallback response
      return http.Response(
          jsonEncode({'message': 'Failed', 'error': e.toString()}), 500);
    }
  }

  Future<Map<String, dynamic>> getData(String url) async {
    try {
      final response = await http.get(Uri.parse(url));

      if (response.statusCode == 200) {
        // Parse and return the JSON data
        return json.decode(response.body) as Map<String, dynamic>;
      } else {
        // Handle non-200 responses
        throw Exception('Failed to load data: ${response.statusCode}');
      }
    } catch (e) {
      // Handle errors such as network issues
      throw Exception('Error occurred: $e');
    }
  }
}
