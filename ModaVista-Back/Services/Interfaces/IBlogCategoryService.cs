using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.BlogCategory;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IBlogCategoryService
    {
        Task<List<BlogCategory>> GetAllAsync();
        Task CreateAsync(BlogCategoryCreateVM request);
        Task DeleteAsync(int id);
        Task<BlogCategory> GetByIdAsync(int id);
        Task EditAsync(int id, BlogCategoryEditVM request);
    }
}
