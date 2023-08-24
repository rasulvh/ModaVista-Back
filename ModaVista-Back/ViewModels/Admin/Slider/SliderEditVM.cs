using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Slider
{
    public class SliderEditVM
    {
        public string? OldImage { get; set; }
        public IFormFile? NewImage { get; set; }
        [Required]
        public string SubHeading { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
