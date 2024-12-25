using System.ComponentModel.DataAnnotations;

namespace Chatappapi.Model
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string activation {  get; set; }
        public string otp { get; set; }
        public DateOnly Date { get; set; }

    }
    public class RegisterDto {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Email {  get; set; }
        public String Password {  get; set; }
        public String Phone {  get; set; }
    };
    public class LoginDTo
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class GetProfile
    {
        public Guid Id { get; set; }
    }
}
