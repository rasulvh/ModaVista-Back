using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.BlogCategory
{
    public class BlogCategoryEditVM
    {
        [Required]
        public string Name { get; set; }
    }
}
