using Microsoft.EntityFrameworkCore;
using ModaVista_Back.Data;
using ModaVista_Back.Models;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels.Admin.Setting;

namespace ModaVista_Back.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(SettingCreateVM request)
        {
            Setting setting = new()
            {
                Key = request.Key,
                Value = request.Value,
            };

            _context.Settings.Add(setting);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var setting = await _context.Settings.FindAsync(id);

            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, SettingEditVM request)
        {
            var setting = await _context.Settings.FindAsync(id);

            setting.Value = request.Value;
            setting.Key = request.Key;

            _context.Settings.Update(setting);
            await _context.SaveChangesAsync();
        }

        public Dictionary<string, string> GetAll() => _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);


        public List<Setting> GetAllWithIds() => _context.Settings.AsEnumerable().ToList();

        public async Task<Setting> GetByIdAsync(int id) => await _context.Settings.FindAsync(id);
    }
}
