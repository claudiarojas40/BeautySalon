using BeautySalon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace BeautySalon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BDContext _context; // Agrega el contexto de la base de datos

        public HomeController(ILogger<HomeController> logger, BDContext context)
        {
            _logger = logger;
            _context = context; // Inicializa el contexto
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View("Index");
            }

            // Realizar b�squeda en varias tablas
            var usuarios = _context.Usuario.Where(u => u.Nombre.Contains(query)).ToList();
            var productos = _context.Producto.Where(p => p.Nombre.Contains(query)).ToList();

            var searchResults = new SearchViewModel
            {
                Usuarios = usuarios,
                Productos = productos
            };

            return View("SearchResults", searchResults);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
