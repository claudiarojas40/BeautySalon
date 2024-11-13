using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeautySalon.Models;
using Microsoft.AspNetCore.Authorization;

namespace BeautySalon.Controllers
{
    [Authorize]
    public class VentaController : Controller
    {
        private readonly BDContext _context;

        public VentaController(BDContext context)
        {
            _context = context;
        }
        // Acción de búsqueda
        public async Task<IActionResult> BuscarProducto(string termino)
        {
            var productos = await _context.Producto
                .Where(p => p.Nombre.Contains(termino))
                .ToListAsync();

            return Json(productos);
        }
        // GET: Venta
        public async Task<IActionResult> Index()
        {
            var bDContext = _context.Venta.Include(v => v.IdUsuarioNavigation);
            return View(await bDContext.ToListAsync());
        }

        // GET: Venta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Venta/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id");
            return View();
        }

        // POST: Venta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VentasExentas,Descuento,Iva,Fecha,Total,TotalPagar,Codigo,Nombre,Direccion,IdUsuario")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", venta.IdUsuario);
            return View(venta);
        }

        // GET: Venta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", venta.IdUsuario);
            return View(venta);
        }

        // POST: Venta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VentasExentas,Descuento,Iva,Fecha,Total,TotalPagar,Codigo,Nombre,Direccion,IdUsuario")] Venta venta)
        {
            if (id != venta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.Id))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", venta.IdUsuario);
            return View(venta);
        }

        private bool VentaExists(int id)
        {
            return _context.Venta.Any(e => e.Id == id);
        }


        // GET: Venta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Venta.FindAsync(id);
            if (venta != null)
            {
                try
                {
                    _context.Venta.Remove(venta);
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
    }
}
