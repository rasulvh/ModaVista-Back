using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels;

namespace ModaVista_Back.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IBrandService _brandService;
        private readonly ITagService _tagService;

        public ShopController(IProductService productService,
                              IProductCategoryService productCategoryService,
                              IBrandService brandService,
                              ITagService tagService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _brandService = brandService;
            _tagService = tagService;

        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithIncludesAsync();
            var categories = await _productCategoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();
            var tags = await _tagService.GetAllAsync();

            ShopVM model = new()
            {
                Brands = brands,
                Categories = categories,
                Products = products.OrderByDescending(m => m.Id).Take(9).ToList(),
                Tags = tags
            };

            return View(model);
        }
    }
}
