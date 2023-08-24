using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Helpers;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Blog;
using ModaVista_Back.ViewModels.Admin.Slider;

namespace ModaVista_Back.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(SliderCreateVM request)
        {
            string image = string.Empty;

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "assets/img/home");

            image = fileName;

            Slider slider = new()
            {
                Heading = request.Heading,
                Image = image,
                SubHeading = request.SubHeading,
                Text = request.Text
            };

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "assets/img/home", slider.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int id, SliderEditVM request)
        {
            var slider = await _context.Sliders.FindAsync(id);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                slider.Image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "assets/img/home");
            }

            slider.Heading = request.Heading;
            slider.Text = request.Text;
            slider.SubHeading = request.SubHeading;

            _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Slider>> GetAllAsync() => await _context.Sliders.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Slider> GetByIdAsync(int id) => await _context.Sliders.FindAsync(id);
    }
}
