using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Data;
using ModaVista_Back.ViewModels;

namespace ModaVista_Back.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var datas = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            ContactVM model = new()
            {
                SettingDatas = datas
            };

            return View(model);
        }
    }
}
