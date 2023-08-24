using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Services;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Faq;

namespace ModaVista_Back.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FaqController : Controller
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        public async Task<IActionResult> Index()
        {
            var faqs = await _faqService.GetAllAsync();
            List<FaqIndexVM> model = new();

            foreach (var item in faqs)
            {
                model.Add(new FaqIndexVM { Heading = item.Heading, Id = item.Id, Text = item.Text });
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
        public async Task<IActionResult> Create(FaqCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _faqService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) BadRequest();

            await _faqService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var faq = await _faqService.GetByIdAsync((int)id);

            if (faq is null) return NotFound();

            FaqEditVM model = new()
            {
                Text = faq.Text,
                Heading = faq.Heading
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FaqEditVM request)
        {
            var faq = await _faqService.GetByIdAsync(id);

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

            await _faqService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
