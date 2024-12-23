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
        public String Name;
        public String Email;
        public String Password;
        public String Phone;
    };
    public class LoginDTo
    {
        public string Email;
        public string Password;
    }
}
