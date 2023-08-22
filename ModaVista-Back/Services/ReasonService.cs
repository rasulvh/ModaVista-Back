using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;

namespace ModaVista_Back.Services
{
    public class ReasonService : IReasonService
    {
        private readonly AppDbContext _context;

        public ReasonService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reason>> GetAllAsync() => await _context.Reasons.Where(m => !m.SoftDelete).ToListAsync();
    }
}
