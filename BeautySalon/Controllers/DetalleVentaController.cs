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
    public class DetalleVentaController : Controller
    {
        private readonly BDContext _context;

        public DetalleVentaController(BDContext context)
        {
            _context = context;
        }

        // GET: DetalleVenta
        public async Task<IActionResult> Index()
        {
            var bDContext = _context.DetalleVenta.Include(d => d.IdProductoNavigation).Include(d => d.IdVentaNavigation);
            return View(await bDContext.ToListAsync());
        }

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
    }
}
