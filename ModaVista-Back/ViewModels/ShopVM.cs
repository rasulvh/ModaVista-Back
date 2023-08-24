using ModaVista_Back.Helpers;
using ModaVista_Back.Models;

namespace ModaVista_Back.ViewModels
{
    public class ShopVM
    {
        public List<Product>? SearchedProducts { get; set; }
        public List<ProductCategory> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Tag> Tags { get; set; }
        public Paginate<Product> Paginate { get; set; }
    }
}
