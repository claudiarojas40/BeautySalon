using BeautySalon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BeautySalon.Controllers
{
    public class SearchProductsController : Controller
    {
        private readonly BDContext _context;

        public SearchProductsController(BDContext context)
        {
            _context = context;
        }

        public IActionResult SearchProducts(string searchTerm)
        {
            var products = _context.Producto
                .Where(p => p.Nombre.Contains(searchTerm))
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.PrecioUnitario
                }) 
                .ToList();
            
            return Json(products);
        }
    }
}
