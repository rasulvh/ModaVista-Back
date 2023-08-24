using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RepeatPassword { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
