using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;

namespace ModaVista_Back.Services
{
    public class FirmService : IFirmService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FirmService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Firm>> GetAllAsync() => await _context.Firms.Where(m => !m.SoftDelete).ToListAsync();
    }
}
