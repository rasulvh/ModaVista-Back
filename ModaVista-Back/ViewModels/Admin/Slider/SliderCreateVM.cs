using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string SubHeading { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
