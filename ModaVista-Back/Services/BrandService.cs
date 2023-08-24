using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Brand;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(BrandCreateVM request)
        {
            Brand brand = new()
            {
                Name = request.Name
            };

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, BrandEditVM request)
        {
            var brand = await _context.Brands.FindAsync(id);

            brand.Name = request.Name;

            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Brand>> GetAllAsync() => await _context.Brands.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Brand> GetByIdAsync(int id) => await _context.Brands.FindAsync(id);
    }
}
