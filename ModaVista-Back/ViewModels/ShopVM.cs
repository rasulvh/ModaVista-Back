using ModaVista_Back.Models;

namespace ModaVista_Back.ViewModels
{
    public class ShopVM
    {
        public List<Product> Products { get; set; }
        public List<ProductCategory> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Tag> Tags { get; set; }
        public bool LoadMore { get; set; }
    }
}
