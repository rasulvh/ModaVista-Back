using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Services;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.BlogCategory;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogCategoryController : Controller
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var blogCategories = await _blogCategoryService.GetAllAsync();
            List<BlogCategoryIndexVM> model = new();

            foreach (var item in blogCategories)
            {
                model.Add(new BlogCategoryIndexVM { Id = item.Id, Name = item.Name });
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
        public async Task<IActionResult> Create(BlogCategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _blogCategoryService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) BadRequest();

            await _blogCategoryService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var blogCategory = await _blogCategoryService.GetByIdAsync((int)id);

            if (blogCategory is null) return NotFound();

            BlogCategoryEditVM model = new()
            {
                Name = blogCategory.Name,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogCategoryEditVM request)
        {
            var blogCategory = await _blogCategoryService.GetByIdAsync(id);

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage);
                foreach (var item in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(request);
            }

            await _blogCategoryService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
