using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Helpers;
using ModaVista_Back.Services;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Blog;
using ModaVista_Back.ViewModels.Admin.Slider;

namespace ModaVista_Back.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            List<SliderIndexVM> model = new();

            foreach (var item in sliders)
            {
                model.Add(new SliderIndexVM
                {
                    Heading = item.Heading,
                    Image = item.Image,
                    SubHeading = item.SubHeading,
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
        public async Task<IActionResult> Create(SliderCreateVM request)
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

            await _sliderService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) BadRequest();

            await _sliderService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            SliderEditVM model = new()
            {
                Text = slider.Text,
                SubHeading = slider.SubHeading,
                Heading = slider.Heading,
                OldImage = slider.Image
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderEditVM request)
        {
            var slider = await _sliderService.GetByIdAsync(id);

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage);
                foreach (var item in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                request.OldImage = slider.Image;
                return View(request);
            }

            if (request.NewImage != null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File format must be image");
                    request.OldImage = slider.Image;
                    return View(request);
                }


                if (request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size can be maximum 200 KB");
                    request.OldImage = slider.Image;
                    return View(request);
                }
            }

            await _sliderService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            SliderDetailVM model = new()
            {
                Image = slider.Image,
                Heading = slider.Heading,
                SubHeading = slider.SubHeading,
                Text = slider.Text
            };

            return View(model);
        }
    }
}
