using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Faq;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var tags = await _tagService.GetAllAsync();
            List<TagIndexVM> model = new();

            foreach (var item in tags)
            {
                model.Add(new TagIndexVM { Id = item.Id, Name = item.Name });
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
        public async Task<IActionResult> Create(TagCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _tagService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) BadRequest();

            await _tagService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var tag = await _tagService.GetByIdAsync((int)id);

            if (tag is null) return NotFound();

            TagEditVM model = new()
            {
                Name = tag.Name,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TagEditVM request)
        {
            var tag = await _tagService.GetByIdAsync(id);

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

            await _tagService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
