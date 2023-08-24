using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Blog
{
    public class BlogCreateVM
    {
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public int BlogCategoryId { get; set; }
    }
}
