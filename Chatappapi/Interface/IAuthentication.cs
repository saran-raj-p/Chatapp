using Chatappapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Interface
{
    public interface IAuthentication
    {
        
            public Task<LoginDTo> UserData(LoginDTo login);
            public Task<LoginDTo> UserRegister(RegisterDto register);

            public  Task<ForgotPasswordResponse> ProcessForgotPassword(string email);
            public Task<bool> VerifyOTP(string email, string otp);
            public Task<bool> UpdatePassword(string email, string newPassword);


        //  public Task SendEmailAsync(string toEmail, string subject, string body);
        public string saveRefreshToken(String refreshtoken, Guid id);
            public String otpGenerate();
            public Task<Getotp> UserActivation(Getotp getotp);
            public  Task<bool> validateRefreshToken(String token);
    }

    



}
