using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Helpers;
using ModaVista_Back.Services;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Firm;
using ModaVista_Back.ViewModels.Admin.ProductCategory;

namespace ModaVista_Back.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var productCategories = await _productCategoryService.GetAllAsync();
            List<ProductCategoryIndexVM> model = new();

            foreach (var item in productCategories)
            {
                model.Add(new ProductCategoryIndexVM { Id = item.Id, Image = item.Image, Name = item.Name });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Please select only image file");
                return View();
            }

            if (request.Image.CheckFileSize(800))
            {
                ModelState.AddModelError("Image", "Image size must be max 800 KB");
                return View();
            }

            await _productCategoryService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) BadRequest();

            await _productCategoryService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var productCategory = await _productCategoryService.GetByIdAsync((int)id);

            if (productCategory is null) return NotFound();

            ProductCategoryEditVM model = new()
            {
                OldImage = productCategory.Image,
                Name = productCategory.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCategoryEditVM request)
        {
            var productCategory = await _productCategoryService.GetByIdAsync(id);

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage);
                foreach (var item in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                request.OldImage = productCategory.Image;
                return View(request);
            }

            if (request.NewImage != null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File format must be image");
                    request.OldImage = productCategory.Image;
                    return View(request);
                }


                if (request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size can be maximum 200 KB");
                    request.OldImage = productCategory.Image;
                    return View(request);
                }
            }

            await _productCategoryService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
