using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;

namespace ModaVista_Back.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Product>> GetAllAsync() => await _context.Products.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<List<Product>> GetAllMenAsync()
        {
            var products = await _context.Products.Where(m => !m.SoftDelete).Include(m => m.Brand).Include(m => m.ProductCategory).Include(m => m.Tag).ToListAsync();

            List<Product> eligibleProducts = new();

            foreach (var item in products)
            {
                if (item.ProductCategory.Name.ToLower().Trim().Contains("men") || item.ProductCategory.Name.ToLower().Trim().Contains("man"))
                {
                    if (!item.ProductCategory.Name.ToLower().Trim().Contains("wo"))
                    {
                        eligibleProducts.Add(item);
                    }
                }
            }

            return eligibleProducts;
        }

        public async Task<List<Product>> GetAllWithIncludesAsync() => await _context.Products.Where(m => !m.SoftDelete).Include(m => m.Brand).Include(m => m.ProductCategory).Include(m => m.Tag).ToListAsync();

        public async Task<List<Product>> GetAllWomenAsync()
        {
            var products = await _context.Products.Where(m => !m.SoftDelete).Include(m => m.Brand).Include(m => m.ProductCategory).Include(m => m.Tag).ToListAsync();

            List<Product> eligibleProducts = new();

            foreach (var item in products)
            {
                if (item.ProductCategory.Name.ToLower().Trim().Contains("women") || item.ProductCategory.Name.ToLower().Trim().Contains("woman"))
                {
                    eligibleProducts.Add(item);
                }
            }

            return eligibleProducts;
        }
    }
}
