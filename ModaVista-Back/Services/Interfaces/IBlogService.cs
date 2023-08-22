using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
    }
}
