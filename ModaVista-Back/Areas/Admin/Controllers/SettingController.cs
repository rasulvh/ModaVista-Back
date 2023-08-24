using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Setting;

namespace ModaVista_Back.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public IActionResult Index()
        {
            var settings = _settingService.GetAllWithIds();
            List<SettingIndexVM> model = new();

            foreach (var item in settings)
            {
                model.Add(new SettingIndexVM
                {
                    Key = item.Key,
                    Value = item.Value,
                    Id = item.Id
                });
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
        public async Task<IActionResult> Create(SettingCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _settingService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            await _settingService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var setting = await _settingService.GetByIdAsync((int)id);

            if (setting is null) return NotFound();

            SettingEditVM model = new()
            {
                Key = setting.Key,
                Value = setting.Value
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SettingEditVM request)
        {
            var setting = await _settingService.GetByIdAsync(id);

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

            await _settingService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
