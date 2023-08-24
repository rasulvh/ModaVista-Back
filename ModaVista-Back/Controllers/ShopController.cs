﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(int page = 1, string searchText = null)
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
                Tags = tags,
            };

            if (searchText == null)
            {
                return View(model);
            }

            List<Product> searchProducts = new();

            foreach (var item in products)
            {
                if (item.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                {
                    searchProducts.Add(item);
                }
            }

            model.Products = searchProducts;

            return View(model);
        }

        private async Task<int> GetCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();

            var pageCount = Math.Ceiling((decimal)count / take);

            return (int)pageCount;
        }
    }
}
