using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Brand
{
    public class BrandEditVM
    {
        [Required]
        public string Name { get; set; }
    }
}
