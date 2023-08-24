using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
