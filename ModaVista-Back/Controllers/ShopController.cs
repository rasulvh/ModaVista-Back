using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Helpers;
using ModaVista_Back.Models;
using ModaVista_Back.Services;
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
        private readonly ISettingService _settingService;

        public ShopController(IProductService productService,
                              IProductCategoryService productCategoryService,
                              IBrandService brandService,
                              ITagService tagService,
                              ISettingService settingService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _brandService = brandService;
            _tagService = tagService;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index(int page = 1, string searchText = null, int? productCategoryId = null)
        {
            var settingDatas = _settingService.GetAll();

            int take = int.Parse(settingDatas["ShopPaginateTakeCount"]);

            var paginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);

            var pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            Paginate<Product> result = new(paginatedDatas, page, pageCount);

            var products = await _productService.GetAllWithIncludesAsync();
            var categories = await _productCategoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();
            var tags = await _tagService.GetAllAsync();

            ShopVM model = new()
            {
                Brands = brands,
                Categories = categories,
                Tags = tags,
                Paginate = result,
            };

            if (searchText != null)
            {
                List<Product> searchProducts = new();

                foreach (var item in products)
                {
                    if (item.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                    {
                        searchProducts.Add(item);
                    }
                }

                model.SearchedProducts = searchProducts;
            }

            if (productCategoryId != null)
            {
                List<Product> categorizedProducts = new();

                foreach (var item in products)
                {
                    if (item.ProductCategoryId == productCategoryId)
                    {
                        categorizedProducts.Add(item);
                    }
                }

                model.CategorizedProducts = categorizedProducts;
            }

            return View(model);

        }

        private async Task<int> GetCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();

            var pageCount = Math.Ceiling((decimal)count / take);

            return (int)pageCount;
        }

        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _productService.GetByIdWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            SingleProductDetailVM model = new()
            {
                Brand = product.Brand.Name,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                ProductCategory = product.ProductCategory.Name,
                Tag = product.Tag.Name
            };

            return View(model);
        }
    }
}
