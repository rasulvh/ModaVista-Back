using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Reason
{
    public class ReasonEditVM
    {
        [Required]
        public string IconClass { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
