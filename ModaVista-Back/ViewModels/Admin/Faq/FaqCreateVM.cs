using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Faq
{
    public class FaqCreateVM
    {
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
