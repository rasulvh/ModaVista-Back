using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Product
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
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
