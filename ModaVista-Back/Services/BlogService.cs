using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Helpers;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Blog;
using ModaVista_Back.ViewModels.Admin.ProductCategory;

namespace ModaVista_Back.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(BlogCreateVM request)
        {
            string image = string.Empty;

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "assets/img/blog");

            image = fileName;

            Blog blog = new()
            {
                BlogCategoryId = request.BlogCategoryId,
                Heading = request.Heading,
                Image = image,
                Text = request.Text
            };

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "assets/img/blog", blog.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int id, BlogEditVM request)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                blog.Image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "assets/img/blog");
            }

            blog.Text = request.Text;
            blog.Heading = request.Heading;
            blog.BlogCategoryId = request.BlogCategoryId;

            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Blog>> GetAllAsync() => await _context.Blogs.Where(m => !m.SoftDelete).Include(m => m.BlogCategory).ToListAsync();

        public async Task<Blog> GetByIdAsync(int id) => await _context.Blogs.Where(m => m.Id == id).Include(m => m.BlogCategory).FirstOrDefaultAsync();
    }
}
