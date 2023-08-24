using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Product
{
    public class ProductEditVM
    {
        [Required]
        public string Name { get; set; }
        public string? OldImage { get; set; }
        public IFormFile? NewImage { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int ProductCategoryId { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int TagId { get; set; }
    }
}
