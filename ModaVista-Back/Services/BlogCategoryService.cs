using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.BlogCategory;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Services
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly AppDbContext _context;

        public BlogCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(BlogCategoryCreateVM request)
        {
            BlogCategory blogCategory = new()
            {
                Name = request.Name
            };

            await _context.BlogCategories.AddAsync(blogCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var blogCategory = await _context.BlogCategories.FindAsync(id);

            _context.BlogCategories.Remove(blogCategory);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, BlogCategoryEditVM request)
        {
            var blogCategory = await _context.BlogCategories.FindAsync(id);

            blogCategory.Name = request.Name;

            _context.BlogCategories.Update(blogCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BlogCategory>> GetAllAsync() => await _context.BlogCategories.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<BlogCategory> GetByIdAsync(int id) => await _context.BlogCategories.FindAsync(id);
    }
}
