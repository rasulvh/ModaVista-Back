using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Helpers;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Firm;
using System.Reflection.Metadata;

namespace ModaVista_Back.Services
{
    public class FirmService : IFirmService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FirmService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(FirmCreateVM request)
        {
            string image = string.Empty;

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "assets/img/home/logo-carousel");

            image = fileName;

            Firm firm = new()
            {
                Image = image
            };

            await _context.Firms.AddAsync(firm);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var firm = await _context.Firms.FindAsync(id);

            _context.Firms.Remove(firm);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "assets/img/home/logo-carousel", firm.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int id, FirmEditVM request)
        {
            var firm = await _context.Firms.FindAsync(id);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                firm.Image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "assets/img/home/logo-carousel");
            }

            _context.Firms.Update(firm);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Firm>> GetAllAsync() => await _context.Firms.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Firm> GetByIdAsync(int id) => await _context.Firms.FindAsync(id);
    }
}
