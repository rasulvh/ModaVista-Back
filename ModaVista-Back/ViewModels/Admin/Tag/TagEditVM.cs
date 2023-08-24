using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Tag
{
    public class TagEditVM
    {
        [Required]
        public string Name { get; set; }
    }
}
