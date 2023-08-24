using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Blog
{
    public class BlogEditVM
    {
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Text { get; set; }
        public string? OldImage { get; set; }
        public IFormFile? NewImage { get; set; }
        public int BlogCategoryId { get; set; }
    }
}
