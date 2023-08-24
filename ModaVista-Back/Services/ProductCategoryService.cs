using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Helpers;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Firm;
using ModaVista_Back.ViewModels.Admin.ProductCategory;

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

        public async Task CreateAsync(ProductCategoryCreateVM request)
        {
            string image = string.Empty;

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "assets/img/home");

            image = fileName;

            ProductCategory productCategory = new()
            {
                Image = image,
                Name = request.Name
            };

            await _context.ProductCategories.AddAsync(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "assets/img/home", productCategory.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int id, ProductCategoryEditVM request)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                productCategory.Image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "assets/img/home");
            }

            productCategory.Name = request.Name;

            _context.ProductCategories.Update(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductCategory>> GetAllAsync() => await _context.ProductCategories.Where(m => !m.SoftDelete).Include(m => m.Products).ToListAsync();

        public async Task<ProductCategory> GetByIdAsync(int id) => await _context.ProductCategories.FindAsync(id);
    }
}
