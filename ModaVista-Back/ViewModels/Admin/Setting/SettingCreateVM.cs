using System.ComponentModel.DataAnnotations;

namespace ModaVista_Back.ViewModels.Admin.Setting
{
    public class SettingCreateVM
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
