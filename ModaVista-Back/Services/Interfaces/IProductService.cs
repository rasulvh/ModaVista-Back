using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllWithIncludesAsync();
        Task<List<Product>> GetAllWomenAsync();
        Task<List<Product>> GetAllMenAsync();
    }
}
