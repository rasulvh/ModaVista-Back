using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Helpers;
using ModaVista_Back.Services;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Firm;

namespace ModaVista_Back.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FirmController : Controller
    {
        private readonly IFirmService _firmService;

        public FirmController(IFirmService firmService)
        {
            _firmService = firmService;
        }

        public async Task<IActionResult> Index()
        {
            var firms = await _firmService.GetAllAsync();
            List<FirmIndexVM> model = new();

            foreach (var item in firms)
            {
                model.Add(new FirmIndexVM { Id = item.Id, Image = item.Image });
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
        public async Task<IActionResult> Create(FirmCreateVM request)
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

            await _firmService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) BadRequest();

            await _firmService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var firm = await _firmService.GetByIdAsync((int)id);

            if (firm is null) return NotFound();

            FirmEditVM model = new()
            {
                OldImage = firm.Image,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FirmEditVM request)
        {
            var firm = await _firmService.GetByIdAsync(id);

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage);
                foreach (var item in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                request.OldImage = firm.Image;
                return View(request);
            }

            if (request.NewImage != null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File format must be image");
                    request.OldImage = firm.Image;
                    return View(request);
                }


                if (request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size can be maximum 200 KB");
                    request.OldImage = firm.Image;
                    return View(request);
                }
            }

            await _firmService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
