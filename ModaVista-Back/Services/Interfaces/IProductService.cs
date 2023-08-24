using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.Blog;
using ModaVista_Back.ViewModels.Admin.Product;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllWithIncludesAsync();
        Task<List<Product>> GetAllWomenAsync();
        Task<List<Product>> GetAllMenAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> GetByIdWithIncludesAsync(int id);
        Task CreateAsync(ProductCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(int id, ProductEditVM request);
        Task<int> GetCountAsync();
        Task<List<Product>> GetPaginatedDatasAsync(int page, int take);
    }
}
