using Microsoft.AspNetCore.Mvc;

namespace ModaVista_Back.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
