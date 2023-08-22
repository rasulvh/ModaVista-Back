using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IReasonService
    {
        Task<List<Reason>> GetAllAsync();
    }
}
