using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeautySalon.Models;
using Microsoft.AspNetCore.Authorization;

namespace BeautySalon.Controllers
{
    [Authorize]
    public class DetalleVentaController : Controller
    {
        private readonly BDContext _context;

        public DetalleVentaController(BDContext context)
        {
            _context = context;
        }

        //Accion para procesar los datos ingresados
        [HttpPost]
        public async Task<IActionResult> ProcesarVenta(Venta pVenta, List<DetalleVenta> pDetalleVentas)
        {
            pVenta.IdUsuario = Global.GIdUsuario;
            _context.Add(pVenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: DetalleVenta
        //public async Task<IActionResult> Index()
        // {
        // var bDContext = _context.DetalleVenta.Include(d => d.IdProductoNavigation).Include(d => d.IdVentaNavigation);
        //  return View(await bDContext.ToListAsync());
        //}

        // GET: DetalleVenta/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVenta
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        // GET: DetalleVenta/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Producto, "Id", "Id");
            ViewData["IdVenta"] = new SelectList(_context.Venta, "Id", "Id");
            return View();
        }

        // POST: DetalleVenta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Precio,Suma,IdProducto,IdVenta,Cantidad")] DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Producto, "Id", "Id", detalleVenta.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Venta, "Id", "Id", detalleVenta.IdVenta);
            return View(detalleVenta);
        }

        // GET: DetalleVenta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVenta.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Producto, "Id", "Id", detalleVenta.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Venta, "Id", "Id", detalleVenta.IdVenta);
            return View(detalleVenta);
        }

        // POST: DetalleVenta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Precio,Suma,IdProducto,IdVenta,Cantidad")] DetalleVenta detalleVenta)
        {
            if (id != detalleVenta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleVentaExists(detalleVenta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Producto, "Id", "Id", detalleVenta.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Venta, "Id", "Id", detalleVenta.IdVenta);
            return View(detalleVenta);
        }

        // GET: DetalleVenta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVenta
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        // POST: DetalleVenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleVenta = await _context.DetalleVenta.FindAsync(id);
            if (detalleVenta != null)
            {
                try
                {
                    _context.DetalleVenta.Remove(detalleVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    // Captura el error y muestra un mensaje de advertencia
                    TempData["ErrorMessage"] = "Este registro no se puede borrar porque tiene dependencias en otras tablas.";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleVentaExists(int id)
        {
            return _context.DetalleVenta.Any(e => e.Id == id);
        }
        public async Task<IActionResult> DetalleVenta(int idVenta)
        {
            var venta = await _context.Venta
                .Include(v => v.DetalleVenta)
                .ThenInclude(dv => dv.IdProducto)
                .FirstOrDefaultAsync(v => v.Id == idVenta);

            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }



        // Acción GET para mostrar la vista de detalle de venta
        [HttpGet]
        public IActionResult Index()
        {
            // Obtener productos de la base de datos y pasarlos a la vista
            var productos = _context.Producto.ToList();
            ViewBag.Producto = productos;
            return View();
        }

        // Acción POST para agregar un producto a la venta
        [HttpPost]
        public async Task<IActionResult> AgregarProducto(int idProducto)
        {
            var producto = await _context.Producto.FindAsync(idProducto);
            if (producto == null)
            {
                TempData["ErrorMessage"] = "Producto no encontrado.";
                return RedirectToAction("DetalleVenta");
            }

            var precioUnitario = producto.PrecioUnitario ?? 0;

            var detalleVenta = new DetalleVenta
            {
                IdProducto = producto.Id, // Cambié a IdProducto para hacer clara la relación con Producto
                Cantidad = 1,
                Precio = precioUnitario,
                Suma = precioUnitario
            };

            _context.DetalleVenta.Add(detalleVenta);
            await _context.SaveChangesAsync();

            return RedirectToAction("DetalleVenta");
        }

        // Acción para buscar productos según el término de búsqueda
        public async Task<IActionResult> BuscarProducto(string termino)
        {
            var productos = await _context.Producto
                .Where(p => p.Nombre.Contains(termino))
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.PrecioUnitario
                })
                .ToListAsync();

            return Json(productos);
        }

        //[HttpPost]
        //public async Task<IActionResult> ProcesarVenta(VentaViewModel model)
        //{
        //    // Crear una nueva instancia de la entidad Venta
        //    var venta = new Venta
        //    {
        //        Fecha = model.Fecha,
        //        Nombre = model.NombreCliente,
        //        Direccion = model.DireccionCliente,
        //        Total = model.Total,
        //        TotalPagar = model.Total, // Ajusta según el modelo si es necesario
        //        DetalleVenta = model.DetalleVenta.Select(d => new DetalleVenta
        //        {
        //            Precio = d.PrecioUnitario, // Mapea PrecioUnitario del ViewModel a Precio del modelo
        //            Suma = d.Subtotal,         // Mapea Subtotal del ViewModel a Suma del modelo
        //            Cantidad = d.Cantidad,
        //            IdProducto = d.IdProducto  // Asume que tienes un campo IdProducto en el ViewModel
        //        }).ToList()
        //    };

        //    // Guardar la venta en la base de datos
        //    _context.Venta.Add(venta);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Index");
        //}

    }
}
