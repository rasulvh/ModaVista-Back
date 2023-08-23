using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IBlogCategoryService
    {
        Task<List<BlogCategory>> GetAllAsync();
    }
}
