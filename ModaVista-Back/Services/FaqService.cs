using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Faq;

namespace ModaVista_Back.Services
{
    public class FaqService : IFaqService
    {
        private readonly AppDbContext _context;

        public FaqService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Faq>> GetAllAsync() => await _context.Faqs.Where(m => !m.SoftDelete).ToListAsync();

        public async Task CreateAsync(FaqCreateVM request)
        {
            Faq faq = new()
            {
                Text = request.Text,
                Heading = request.Heading,
            };

            await _context.Faqs.AddAsync(faq);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var faq = await _context.Faqs.FindAsync(id);

            _context.Faqs.Remove(faq);
            await _context.SaveChangesAsync();
        }

        public async Task<Faq> GetByIdAsync(int id) => await _context.Faqs.FindAsync(id);

        public async Task EditAsync(int id, FaqEditVM request)
        {
            var faq = await _context.Faqs.FindAsync(id);

            faq.Text = request.Text;
            faq.Heading = request.Heading;

            _context.Faqs.Update(faq);
            await _context.SaveChangesAsync();
        }
    }
}
