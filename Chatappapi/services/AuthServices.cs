using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chatappapi.Helpers;
using Chatappapi.Model;
using Microsoft.IdentityModel.Tokens;

namespace Chatappapi.services
{
    public class AuthServices
    {
        public String GenerateToken(LoginDTo login)
        {
            var userId = login.Email;
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Helpers.AuthSettings.PrivateKey);
            var credentials = new SecurityTokenDescriptor { 
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId,"password",login.Password) }),
                Expires = DateTime.UtcNow.AddDays(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
            };
            var token = handler.CreateToken(credentials);
            
            return handler.WriteToken(token);
        }
    }
}
