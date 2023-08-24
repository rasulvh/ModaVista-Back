using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.Blog;
using ModaVista_Back.ViewModels.Admin.Slider;

namespace ModaVista_Back.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task CreateAsync(SliderCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(int id, SliderEditVM request);
    }
}
