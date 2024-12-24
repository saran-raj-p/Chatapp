using Chatappapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Interface
{
    public interface IAuthentication
    {
        
            public Task<int> UserData(LoginDTo login);
            public Task<LoginDTo> UserRegister(RegisterDto register);
        
    }
}
