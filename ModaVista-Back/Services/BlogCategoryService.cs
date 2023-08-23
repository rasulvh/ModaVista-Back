using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;

namespace ModaVista_Back.Services
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly AppDbContext _context;

        public BlogCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BlogCategory>> GetAllAsync() => await _context.BlogCategories.Where(m => !m.SoftDelete).ToListAsync();
    }
}
