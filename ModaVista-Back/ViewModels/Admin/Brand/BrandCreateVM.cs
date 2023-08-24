using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Brand
{
    public class BrandCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
