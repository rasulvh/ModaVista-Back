using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AppUser> GetUserByUsernameOrEmail(string emailOrUsername);
        Task<string> GeneratePasswordResetTokenAsync(AppUser user);
        void SendConfirmationEmail(AppUser user, string email, string subject, string link);
        Task<AppUser> GetUserById(string id);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task ResetPasswordAsync(AppUser user, string token, string password);
    }
}
