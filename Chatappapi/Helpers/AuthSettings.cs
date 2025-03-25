using Microsoft.IdentityModel.Tokens;
namespace Chatappapi.Helpers
{
    public class AuthSettings
    {
        public  string PrivateKey { get; set; }
        public  string Issuer { get; set; }
        public  string Audience { get; set; }
        
        public AuthSettings(IConfiguration configuration) {
            PrivateKey = configuration["jwt:Key"];
            Issuer = configuration["jwt:Issuer"];
            Audience = configuration["jwt:Audience"];
        }

        
    }
}
