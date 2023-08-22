using Microsoft.AspNetCore.Mvc;

namespace ModaVista_Back.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
