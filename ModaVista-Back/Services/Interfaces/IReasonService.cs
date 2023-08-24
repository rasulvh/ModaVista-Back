using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.Reason;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IReasonService
    {
        Task<List<Reason>> GetAllAsync();
        Task CreateAsync(ReasonCreateVM request);
        Task DeleteAsync(int id);
        Task<Reason> GetByIdAsync(int id);
        Task EditAsync(int id, ReasonEditVM request);
    }
}
