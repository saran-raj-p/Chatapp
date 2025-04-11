import 'package:chatappui/services/localstoragemethods.dart' as localstorage;
import 'package:dio/dio.dart';

class httpmethods {
  late final dio;
  httpmethods() {
    dio = Dio(BaseOptions(baseUrl: 'https://localhost:7165/api/'));
    dio.interceptors.add(InterceptorsWrapper(
      onRequest: (options, handler) async {
        String? token =
            (await localstorage.localstoragemethods().getlocal('accessToken')!);
        options.headers['Authorization'] = 'Bearer $token';
        return handler.next(options);
      },
      onError: (error, handler) async {
        if (error.response?.statusCode == 401) {
          String? refreshToken = (await localstorage
              .localstoragemethods()
              .getlocal('refreshToken')!);

          try {
            var refresh = await dio
                .post('Auth/ExpiredToken/', data: {'token': refreshToken});
            if (refresh.statusCode == 200) {
              final newAccessToken = refresh.data['accessToken'];
              localstorage
                  .localstoragemethods()
                  .setLocal('accessToken', newAccessToken);
              error.requestOptions.headers['Authorization'] =
                  'Bearer $newAccessToken';
              final retry = await dio.fetch(error.requestOptions);
              return handler.resolve(retry);
            } else {
              return handler.reject(error);
            }
          } catch (e) {
            return handler.reject(error);
          }
        }
      },
    ));
  }
  Future<Response> postWithData(String url, dynamic bodyData) async {
    try {
      // Ensure the body is JSON-encoded
      //final encodedBody = jsonEncode(bodyData);
      final response = await dio.post(url, data: bodyData);
      switch (response.statusCode) {
        case 200:
          return response;
        case 401:
          return Response(
              requestOptions: response.requestOptions,
              statusCode: 401,
              data: response.data);
        default:
          return Response(
              requestOptions: response.requestOptions, statusCode: 500);
      }
    } catch (e) {
      // Handle errors and return a fallback response
      return Response(
        requestOptions: RequestOptions(path: url),
        statusCode: 500,
        data: {'message': 'Unexpected error', 'error': e.toString()},
      );
    }
  }

  Future<Map<String, dynamic>> getData(String url) async {
    try {
      final response = await dio.get(url);

      if (response.statusCode == 200) {
        // Parse and return the JSON data
        return response.data as Map<String, dynamic>;
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
