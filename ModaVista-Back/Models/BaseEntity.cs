namespace ModaVista_Back.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool SoftDelete { get; set; }
    }
}
