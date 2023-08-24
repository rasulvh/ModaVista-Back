using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.ProductCategory
{
    public class ProductCategoryEditVM
    {
        [Required]
        public string Name { get; set; }
        public IFormFile? NewImage { get; set; }
        public string? OldImage { get; set; }
    }
}
