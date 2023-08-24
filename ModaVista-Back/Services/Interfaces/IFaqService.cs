using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.Faq;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IFaqService
    {
        Task<List<Faq>> GetAllAsync();
        Task CreateAsync(FaqCreateVM request);
        Task DeleteAsync(int id);
        Task<Faq> GetByIdAsync(int id);
        Task EditAsync(int id, FaqEditVM request);
    }
}
