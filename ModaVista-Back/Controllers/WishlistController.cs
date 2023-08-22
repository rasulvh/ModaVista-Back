using Microsoft.AspNetCore.Mvc;

namespace ModaVista_Back.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
