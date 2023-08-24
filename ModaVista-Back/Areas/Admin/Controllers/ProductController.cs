using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModaVista_Back.Helpers;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Product;

namespace ModaVista_Back.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly ITagService _tagService;
        private readonly IBrandService _brandService;

        public ProductController(IProductService productService,
                                 IProductCategoryService productCategoryService,
                                 ITagService tagService,
                                 IBrandService brandService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _tagService = tagService;
            _brandService = brandService;

        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithIncludesAsync();
            List<ProductIndexVM> model = new();

            foreach (var item in products)
            {
                model.Add(new ProductIndexVM
                {
                    Image = item.Image,
                    Name = item.Name,
                    Price = item.Price,
                    ProductCategory = item.ProductCategory.Name,
                    Id = item.Id
                });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetAllSelectOptions();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
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

            await _productService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task GetAllSelectOptions()
        {
            ViewBag.productCategories = await GetAllProductCategories();
            ViewBag.tags = await GetAllTags();
            ViewBag.brands = await GetAllBrands();
        }

        public async Task<SelectList> GetAllProductCategories()
        {
            IEnumerable<ProductCategory> productCategories = await _productCategoryService.GetAllAsync();
            return new SelectList(productCategories, "Id", "Name");
        }

        public async Task<SelectList> GetAllTags()
        {
            IEnumerable<Tag> tags = await _tagService.GetAllAsync();
            return new SelectList(tags, "Id", "Name");
        }

        public async Task<SelectList> GetAllBrands()
        {
            IEnumerable<Brand> brands = await _brandService.GetAllAsync();
            return new SelectList(brands, "Id", "Name");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) BadRequest();

            await _productService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _productService.GetByIdWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            await GetAllSelectOptions();

            ProductEditVM model = new()
            {
                BrandId = product.BrandId,
                Description = product.Description,
                Name = product.Name,
                OldImage = product.Image,
                Price = product.Price,
                ProductCategoryId = product.ProductCategoryId,
                TagId = product.TagId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditVM request)
        {
            var product = await _productService.GetByIdWithIncludesAsync(id);

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage);
                foreach (var item in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                request.OldImage = product.Image;
                return View(request);
            }

            if (request.NewImage != null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File format must be image");
                    request.OldImage = product.Image;
                    return View(request);
                }


                if (request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size can be maximum 200 KB");
                    request.OldImage = product.Image;
                    return View(request);
                }
            }

            await _productService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _productService.GetByIdWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            ProductDetailVM model = new()
            {
                Price = product.Price,
                Brand = product.Brand.Name,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                ProductCategory = product.ProductCategory.Name,
                Tag = product.Tag.Name
            };

            return View(model);
        }
    }
}
