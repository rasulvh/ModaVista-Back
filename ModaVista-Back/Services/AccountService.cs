using Microsoft.AspNetCore.Identity;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;

namespace ModaVista_Back.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<AppUser> GetUserByUsernameOrEmail(string emailOrUsername)
        {
            AppUser user = await _userManager.FindByEmailAsync(emailOrUsername);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(emailOrUsername);
            }

            return user;
        }

        public void SendConfirmationEmail(AppUser user, string email, string subject, string link)
        {
            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/account.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);

            html = html.Replace("{{fullName}}", user.Fullname);

            _emailService.Send(email, subject, html);
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task ResetPasswordAsync(AppUser user, string token, string password)
        {
            await _userManager.ResetPasswordAsync(user, token, password);
        }
    }
}
