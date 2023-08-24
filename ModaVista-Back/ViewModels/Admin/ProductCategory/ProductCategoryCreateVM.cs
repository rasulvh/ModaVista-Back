using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.ProductCategory
{
    public class ProductCategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
