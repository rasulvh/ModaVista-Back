namespace ModaVista_Back.Models
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
