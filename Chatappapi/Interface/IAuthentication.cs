using Chatappapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Interface
{
    public interface IAuthentication
    {
        
            public Task<LoginDTo> UserData(LoginDTo login);
            public Task<LoginDTo> UserRegister(RegisterDto register);
            public string saveRefreshToken(String refreshtoken, Guid id);
            public String otpGenerate();
            public Task<Getotp> UserActivation(Getotp getotp);
            public  Task<bool> validateRefreshToken(String token);
    }
}
