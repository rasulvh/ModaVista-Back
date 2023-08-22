namespace ModaVista_Back.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }
        public Brand Brand { get; set; }
        public int BrandId { get; set; }
        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
