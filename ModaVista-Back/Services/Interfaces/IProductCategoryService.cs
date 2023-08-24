using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.Firm;
using ModaVista_Back.ViewModels.Admin.ProductCategory;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<List<ProductCategory>> GetAllAsync();
        Task<ProductCategory> GetByIdAsync(int id);
        Task CreateAsync(ProductCategoryCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(int id, ProductCategoryEditVM request);
    }
}
