using Microsoft.AspNetCore.Identity;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels;
using System.Security.Claims;

namespace ModaVista_Back.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFirmService _firmService;
        private readonly IProductCategoryService _productCategoryService;

        public LayoutService(AppDbContext context,
                            IHttpContextAccessor accessor,
                            UserManager<AppUser> userManager,
                            IFirmService firmService,
                            IProductCategoryService productCategoryService)
        {
            _context = context;
            _accessor = accessor;
            _userManager = userManager;
            _firmService = firmService;
            _productCategoryService = productCategoryService;
        }

        public async Task<LayoutVM> GetAllDatas()
        {
            var userId = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var datas = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
            var firms = await _firmService.GetAllAsync();
            var productCategory = await _productCategoryService.GetAllAsync();

            return new LayoutVM
            {
                Firms = firms,
                SettingDatas = datas,
                UserFullname = user?.Fullname,
                ProductCategories = productCategory
            };
        }
    }
}
