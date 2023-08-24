using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Helpers;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Blog;
using ModaVista_Back.ViewModels.Admin.Product;

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

        public async Task CreateAsync(ProductCreateVM request)
        {
            string image = string.Empty;

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "assets/img/home/products");

            image = fileName;

            Product product = new()
            {
                BrandId = request.BrandId,
                Description = request.Description,
                Image = image,
                Name = request.Name,
                Price = request.Price,
                ProductCategoryId = request.ProductCategoryId,
                TagId = request.TagId
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "assets/img/home/products", product.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int id, ProductEditVM request)
        {
            var product = await _context.Products.FindAsync(id);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                product.Image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "assets/img/home/products");
            }

            product.ProductCategoryId = request.ProductCategoryId;
            product.Price = request.Price;
            product.BrandId = request.BrandId;
            product.Description = request.Description;
            product.Name = request.Name;
            product.TagId = request.TagId;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
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

        public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        public async Task<Product> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Products.Where(m => m.Id == id).Include(m => m.Brand).Include(m => m.ProductCategory).Include(m => m.Tag).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            var products = await _context.Products.ToListAsync();

            return products.Count();
        }

        public async Task<List<Product>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Products
                    .Where(m => !m.SoftDelete)
                    .Include(m => m.Brand)
                    .Include(m => m.Tag)
                    .Include(m => m.ProductCategory)
                    .Skip((page - 1) * take)
                    .Take(take)
                    .ToListAsync();
        }
    }
}
