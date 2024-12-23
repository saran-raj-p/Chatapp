
using Chatappapi.Interface;
using Chatappapi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Repository
{
    public class Authencation : IAuthentication
    {
        public IActionResult Userlogin(LoginDTo login)
        {
            var email = login.Email;
            var password = login.Password;

        }
    }
}
