using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
