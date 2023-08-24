using ModaVista_Back.Models;
using ModaVista_Back.ViewModels.Admin.Setting;

namespace ModaVista_Back.Services.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetAll();
        List<Setting> GetAllWithIds();
        Task EditAsync(int id, SettingEditVM request);
        Task DeleteAsync(int id);
        Task CreateAsync(SettingCreateVM request);
        Task<Setting> GetByIdAsync(int id);
    }
}
