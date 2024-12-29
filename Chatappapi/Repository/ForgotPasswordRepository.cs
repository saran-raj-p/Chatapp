using Chatappapi.Interface;

namespace Chatappapi.Repository
{
    public class ForgotPasswordRepository : IForgotPasswordRepository
    {
        public async Task<bool> ResetPasswordAsync(string resetToken, string newPassword)
        {
            // Mocked implementation: Replace this with actual database logic
            if (string.IsNullOrEmpty(resetToken))
                return false;

            // Simulate updating the password in the database
            await Task.Delay(100); // Simulating async work
            return true;
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            // Mocked implementation: Replace with actual database logic
            // Check if the email exists in the database
            await Task.Delay(50); // Simulating async work
            return email == "example@example.com"; // Example check
        }
    }
}
