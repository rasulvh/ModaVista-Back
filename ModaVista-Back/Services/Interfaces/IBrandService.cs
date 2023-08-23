using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync();
    }
}
