
using Chatappapi.Interface;
using Chatappapi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Repository
{
    public class Authencation : IAuthentication
    {
        IActionResult IAuthentication.Userlogin(LoginDTo login)
        {
            throw new NotImplementedException();
        }
    }
}
