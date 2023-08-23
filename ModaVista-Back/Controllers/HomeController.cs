using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels;
using System.Diagnostics;

namespace ModaVista_Back.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;
        private readonly ITagService _tagService;
        private readonly IBlogService _blogService;
        private readonly IReasonService _reasonService;
        private readonly IFirmService _firmService;

        public HomeController(ISliderService sliderService,
                              IProductCategoryService productCategoryService,
                              IProductService productService,
                              ITagService tagService,
                              IBlogService blogService,
                              IReasonService reasonService,
                              IFirmService firmService)
        {
            _sliderService = sliderService;
            _productCategoryService = productCategoryService;
            _productService = productService;
            _tagService = tagService;
            _blogService = blogService;
            _reasonService = reasonService;
            _firmService = firmService;

        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var productCategories = await _productCategoryService.GetAllAsync();
            var menProducts = await _productService.GetAllMenAsync();
            var womenProducts = await _productService.GetAllWomenAsync();
            var tags = await _tagService.GetAllAsync();
            var blogs = await _blogService.GetAllAsync();
            var reasons = await _reasonService.GetAllAsync();
            var firms = await _firmService.GetAllAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                ProductCategories = productCategories.OrderByDescending(m => m.Id).Take(3).ToList(),
                MenProducts = menProducts,
                WomenProducts = womenProducts,
                Tags = tags.Take(4).ToList(),
                Blogs = blogs.OrderByDescending(m => m.Id).Take(3).ToList(),
                Reasons = reasons.OrderByDescending(m => m.Id).Take(3).ToList(),
                Firms = firms
            };

            return View(model);
        }
    }
}