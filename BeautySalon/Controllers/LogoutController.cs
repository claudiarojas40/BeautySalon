using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Controllers
{
    public class LagoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
