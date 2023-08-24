using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.Brand;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync();
        Task CreateAsync(BrandCreateVM request);
        Task DeleteAsync(int id);
        Task<Brand> GetByIdAsync(int id);
        Task EditAsync(int id, BrandEditVM request);
    }
}
