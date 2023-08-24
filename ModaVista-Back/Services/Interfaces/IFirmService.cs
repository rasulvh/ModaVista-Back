using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.Firm;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IFirmService
    {
        Task<List<Firm>> GetAllAsync();
        Task<Firm> GetByIdAsync(int id);
        Task CreateAsync(FirmCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(int id, FirmEditVM request);
    }
}
