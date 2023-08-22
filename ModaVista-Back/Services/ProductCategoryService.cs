using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;

namespace ModaVista_Back.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductCategoryService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<ProductCategory>> GetAllAsync() => await _context.ProductCategories.Where(m => !m.SoftDelete).Include(m => m.Products).ToListAsync();
    }
}
