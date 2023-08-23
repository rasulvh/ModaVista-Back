using ModaVista_Back.ViewModels;

namespace ModaVista_Back.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<LayoutVM> GetAllDatas();
    }
}
