using ModaVista_Back.Models;

namespace ModaVista_Back.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Product> MenProducts { get; set; }
        public List<Product> WomenProducts { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Reason> Reasons { get; set; }
    }
}
