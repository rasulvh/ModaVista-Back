using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels
{
    public class RegisterVM
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Fullname is required")]
        public string Fullname { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Repeat password is required")]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string RepeatPassword { get; set; }
    }
}
