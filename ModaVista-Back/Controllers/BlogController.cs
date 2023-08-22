using Microsoft.AspNetCore.Mvc;

namespace ModaVista_Back.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
