using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.BlogCategory
{
    public class BlogCategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
