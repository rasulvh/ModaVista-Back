using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Firm
{
    public class FirmCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
