using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Helpers;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels;
using System.Security.Claims;

namespace ModaVista_Back.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            };

            AppUser user = await _userManager.FindByEmailAsync(request.EmailOrUsername);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.EmailOrUsername);
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(request);
            }

            var comparePassword = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (comparePassword.ToString() == "Failed")
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(request);
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Please confirm your account");
                return View(request);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            AppUser user = new()
            {
                UserName = request.Username,
                Email = request.Email,
                Fullname = request.Fullname,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(request);
            }

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme);

            string html = $"<h1>Please confirm your email <a href={link}>Click here</a></h1>";

            string subject = "Email Confirmation";

            _emailService.Send(user.Email, subject, html);

            return RedirectToAction(nameof(VerifyEmail));
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId is null || token is null)
            {
                return BadRequest();
            }

            AppUser user = await _userManager.FindByIdAsync(userId);

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        public async Task<IActionResult> CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            ProfileVM model = new()
            {
                Fullname = user.Fullname,
                Email = user.Email
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
