using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Faq
{
    public class FaqEditVM
    {
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
