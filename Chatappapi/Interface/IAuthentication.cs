using Chatappapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Interface
{
    public interface IAuthentication
    {
        
            public Task<LoginDTo> UserData(LoginDTo login);
            public Task<LoginDTo> UserRegister(RegisterDto register);

             public Task<bool> IsEmailRegisteredAsync(string email);
            public   Task<bool> SendOtpAsync(string email);
            public  Task<bool> VerifyOtpAsync(string email, string otp);
            public   Task<bool> ResetPasswordAsync(string email, string newPassword);

       //  public Task SendEmailAsync(string toEmail, string subject, string body);
            public string saveRefreshToken(String refreshtoken, Guid id);
            public String otpGenerate();
            public Task<Getotp> UserActivation(Getotp getotp);
            public  Task<bool> validateRefreshToken(String token);
    }

    



}
