using ModaVista_Back.Models;

namespace ModaVista_Back.Services.Interfaces
{
    public interface IFaqService
    {
        Task<List<Faq>> GetAllAsync();
    }
}
