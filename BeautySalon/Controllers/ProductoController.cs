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
    public class ProductoController : Controller
    {
        private readonly BDContext _context;

        public ProductoController(BDContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Producto.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,PrecioUnitario,PrecioVenta,Codigo,Imagen,Marca,FechaRegistro,FechaVencimiento,Categoria")] Producto producto, IFormFile Imagen)
        {
            if (ModelState.IsValid)
            {
                string rutaImagen = null;

                // Verificar si se ha subido una imagen
                if (Imagen != null && Imagen.Length > 0)
                {
                    // Obtener la ruta donde se guardará la imagen
                    string rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

                    // Crear el nombre único para la imagen
                    string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);

                    // Combinar la ruta de la carpeta con el nombre del archivo
                    string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                    // Guardar la imagen en la ruta especificada
                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        await Imagen.CopyToAsync(stream);
                    }

                    // Almacenar la ruta para guardarla en la base de datos o retornarla
                    rutaImagen = "/img/" + nombreArchivo;
                }
                producto.Imagen = rutaImagen;
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,PrecioUnitario,PrecioVenta,Codigo,Imagen,Marca,FechaRegistro,FechaVencimiento,Categoria")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        private bool ProductoExists(int id)
        {
            throw new NotImplementedException();
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                try
                {
                    _context.Producto.Remove(producto);
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
