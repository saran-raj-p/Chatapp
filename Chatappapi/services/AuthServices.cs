using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Chatappapi.Helpers;
using Chatappapi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Chatappapi.services
{
    public class AuthServices
    {
        private readonly AuthSettings _authSettings;

        public AuthServices(AuthSettings authSettings)
        {
            _authSettings = authSettings;
        }

        //Token generated during the login
        public String GenerateToken(LoginDTo login)
        {
            var userId = login.UserId;
            var userEmail = login.Email;
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSettings.PrivateKey);
            var credentials = new SecurityTokenDescriptor { 
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email,userEmail,ClaimTypes.NameIdentifier,userId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
                Issuer = _authSettings.Issuer,
                Audience = _authSettings.Audience
            };
            var token = handler.CreateToken(credentials);
            
            return handler.WriteToken(token);
        }

        //Token Generated When the AccessToken Expires
        public String generateAccessToken(ClaimsPrincipal principal)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.PrivateKey));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
            var claims = principal.Claims;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = credentials,
                Issuer = _authSettings.Issuer,
                Audience = _authSettings.Audience
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        // get data from the token
        public ClaimsPrincipal claimsPrincipalFrom(string token) {
            var tokenhandler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidIssuer = _authSettings.Issuer,
                ValidAudience = _authSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.PrivateKey))
            };
            try
            {
                var principal = tokenhandler.ValidateToken(token, parameters, out var validatedToken);
                return principal;
            }
            catch (Exception ex) {
                return null;
            }

        
        }
        //generate refresh token
        public string generateRefreshToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.PrivateKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                // Extract and validate claims from the old token
                var claims = ExtractClaimsFromToken(token);
                if (claims == null)
                {
                    throw new SecurityTokenException("Invalid or expired token.");
                }

                // Create a new token descriptor for the refresh token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7), // Set refresh token expiration
                    SigningCredentials = credentials,
                    Issuer = _authSettings.Issuer,
                    Audience = _authSettings.Audience
                };

                // Create and return the new refresh token
                var newToken = handler.CreateToken(tokenDescriptor);
                return handler.WriteToken(newToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating refresh token: {ex.Message}");
                throw; // Optionally rethrow or handle the exception as needed
            }
        }
        //Extract details from the previous generated token
        private IEnumerable<Claim> ExtractClaimsFromToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_authSettings.PrivateKey);
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _authSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _authSettings.Audience,
                    ValidateLifetime = false // Allow expired tokens for claim extraction
                };

                var principal = handler.ValidateToken(token, parameters, out SecurityToken validatedToken);

                // Additional check to ensure the token is a JWT
                if (validatedToken is JwtSecurityToken jwtToken &&
                    jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal.Claims;
                }

                throw new SecurityTokenException("Invalid token.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting claims from token: {ex.Message}");
                return null; // Handle token validation failures
            }
        }

    }
}
