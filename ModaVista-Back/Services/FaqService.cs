using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;

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
    }
}
