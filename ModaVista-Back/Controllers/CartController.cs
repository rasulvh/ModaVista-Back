using Microsoft.AspNetCore.Mvc;

namespace ModaVista_Back.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
