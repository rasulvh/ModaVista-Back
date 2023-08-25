using ModaVista_Back.Models;

namespace ModaVista_Back.ViewModels
{
    public class SingleProductDetailVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProductCategory { get; set; }
        public string Brand { get; set; }
        public string Tag { get; set; }
    }
}
