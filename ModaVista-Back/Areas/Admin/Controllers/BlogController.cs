using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModaVista_Back.Helpers;
using ModaVista_Back.Models;
using ModaVista_Back.Services;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Blog;
using ModaVista_Back.ViewModels.Admin.ProductCategory;

namespace ModaVista_Back.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogController(IBlogService blogService,
                              IBlogCategoryService blogCategoryService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsync();
            List<BlogIndexVM> model = new();

            foreach (var item in blogs)
            {
                model.Add(new BlogIndexVM
                {
                    BlogCategory = item.BlogCategory.Name,
                    Heading = item.Heading,
                    Id = item.Id,
                    Image = item.Image,
                    Text = item.Text
                });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.blogCategories = await GetAllBlogCategories();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM request)
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

            await _blogService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<SelectList> GetAllBlogCategories()
        {
            IEnumerable<BlogCategory> categories = await _blogCategoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) BadRequest();

            await _blogService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var blog = await _blogService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            ViewBag.blogCategories = await GetAllBlogCategories();

            BlogEditVM model = new()
            {
                BlogCategoryId = blog.BlogCategoryId,
                Heading = blog.Heading,
                OldImage = blog.Image,
                Text = blog.Text
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogEditVM request)
        {
            var blog = await _blogService.GetByIdAsync(id);

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage);
                foreach (var item in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                request.OldImage = blog.Image;
                return View(request);
            }

            if (request.NewImage != null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File format must be image");
                    request.OldImage = blog.Image;
                    return View(request);
                }


                if (request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size can be maximum 200 KB");
                    request.OldImage = blog.Image;
                    return View(request);
                }
            }

            await _blogService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var blog = await _blogService.GetByIdAsync((int)id);

            if (blog is null) return NotFound();

            BlogDetailVM model = new()
            {
                Text = blog.Text,
                BlogCategory = blog.BlogCategory.Name,
                Heading = blog.Heading,
                Image = blog.Image
            };

            return View(model);
        }
    }
}
