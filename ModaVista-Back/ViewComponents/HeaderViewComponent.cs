using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Services.Interfaces;

namespace ModaVista_Back.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;

        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = await _layoutService.GetAllDatas();

            return await Task.FromResult(View(datas));
        }
    }
}
