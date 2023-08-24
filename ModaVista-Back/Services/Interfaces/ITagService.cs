using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.Faq;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllAsync();
        Task CreateAsync(TagCreateVM request);
        Task DeleteAsync(int id);
        Task<Tag> GetByIdAsync(int id);
        Task EditAsync(int id, TagEditVM request);
    }
}
