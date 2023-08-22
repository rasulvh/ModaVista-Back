using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllAsync();
    }
}
