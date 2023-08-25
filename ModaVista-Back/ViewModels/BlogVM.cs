using ModaVista_Back.Models;

namespace ModaVista_Back.ViewModels
{
    public class BlogVM
    {
        public List<Blog> Blogs { get; set; }
        public List<Blog> MiniBlogs { get; set; }
        public List<BlogCategory> BlogCategories { get; set; }
        public List<Blog>? SearchedBlogs { get; set; }
        public List<Blog>? CategorizedBlogs { get; set; }
    }
}
