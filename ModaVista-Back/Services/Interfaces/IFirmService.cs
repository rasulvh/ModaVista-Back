using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IFirmService
    {
        Task<List<Firm>> GetAllAsync();
    }
}
