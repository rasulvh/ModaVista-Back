namespace ModaVista_Back.Models
{
    public class Blog : BaseEntity
    {
        public string Heading { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public int BlogCategoryId { get; set; }
    }
}
