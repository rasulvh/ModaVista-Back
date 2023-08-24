using ModaVista_Back.Models;

namespace ModaVista_Back.ViewModels
{
    public class LayoutVM
    {
        public List<Firm> Firms { get; set; }
        public Dictionary<string, string> SettingDatas { get; set; }
        public string UserFullname { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
