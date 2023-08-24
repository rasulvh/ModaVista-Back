using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Reason;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReasonController : Controller
    {
        private readonly IReasonService _reasonService;

        public ReasonController(IReasonService reasonService)
        {
            _reasonService = reasonService;
        }

        public async Task<IActionResult> Index()
        {
            var reasons = await _reasonService.GetAllAsync();
            List<ReasonIndexVM> model = new();

            foreach (var item in reasons)
            {
                model.Add(new ReasonIndexVM { Heading = item.Heading, IconClass = item.IconClass, Id = item.Id, Text = item.Text });
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
        public async Task<IActionResult> Create(ReasonCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _reasonService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) BadRequest();

            await _reasonService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var reason = await _reasonService.GetByIdAsync((int)id);

            if (reason is null) return NotFound();

            ReasonEditVM model = new()
            {
                Text = reason.Text,
                IconClass = reason.IconClass,
                Heading = reason.Heading
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReasonEditVM request)
        {
            var reason = await _reasonService.GetByIdAsync(id);

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

            await _reasonService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
