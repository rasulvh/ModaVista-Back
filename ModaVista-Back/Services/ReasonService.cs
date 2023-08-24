using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Reason;
using ModaVista_Back.ViewModels.Admin.Tag;

namespace ModaVista_Back.Services
{
    public class ReasonService : IReasonService
    {
        private readonly AppDbContext _context;

        public ReasonService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ReasonCreateVM request)
        {
            Reason reason = new()
            {
                Heading = request.Heading,
                IconClass = request.IconClass,
                Text = request.Text,
            };

            await _context.Reasons.AddAsync(reason);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reason = await _context.Reasons.FindAsync(id);

            _context.Reasons.Remove(reason);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, ReasonEditVM request)
        {
            var reason = await _context.Reasons.FindAsync(id);

            reason.Text = request.Text;
            reason.IconClass = request.IconClass;
            reason.Heading = request.Heading;

            _context.Reasons.Update(reason);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Reason>> GetAllAsync() => await _context.Reasons.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Reason> GetByIdAsync(int id) => await _context.Reasons.FindAsync(id);
    }
}
