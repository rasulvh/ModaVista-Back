using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TagCreateVM request)
        {
            Tag tag = new()
            {
                Name = request.Name
            };

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, TagEditVM request)
        {
            var tag = await _context.Tags.FindAsync(id);

            tag.Name = request.Name;

            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetAllAsync() => await _context.Tags.Where(m => !m.SoftDelete).Include(m => m.Products).ToListAsync();

        public async Task<Tag> GetByIdAsync(int id) => await _context.Tags.FindAsync(id);
    }
}
