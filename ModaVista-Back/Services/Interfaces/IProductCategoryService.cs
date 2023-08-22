using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<List<ProductCategory>> GetAllAsync();
    }
}
