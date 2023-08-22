using Microsoft.AspNetCore.Mvc;

namespace ModaVista_Back.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
