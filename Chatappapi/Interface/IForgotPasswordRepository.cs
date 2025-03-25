namespace Chatappapi.Interface
{
    public interface IForgotPasswordRepository
    {
        Task<bool> ResetPasswordAsync(string resetToken, string newPassword);
        Task<bool> IsEmailRegisteredAsync(string email);
    }
}
