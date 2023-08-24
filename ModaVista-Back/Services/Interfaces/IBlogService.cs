using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.Blog;
using ModaVista_Back.ViewModels.Admin.ProductCategory;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task CreateAsync(BlogCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(int id, BlogEditVM request);
    }
}
