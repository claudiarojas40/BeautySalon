using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautySalon.Models;
using System.Drawing.Printing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace BeautySalon.Controllers
{
    public class CitasController : Controller
    {
        private readonly BDContext _context;

        public CitasController(BDContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Citas.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCompleto,CorreoElectronico,NumeroContacto,Servicios,FechaRegistro")] Citas cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,CorreoElectronico,NumeroContacto,Servicios,FechaRegistro")] Citas cita)
        {
            if (id != cita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.Id))
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
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ExportarPDF()
        {
            var citas = await _context.Citas.ToListAsync();

            using (MemoryStream ms = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter.GetInstance(pdfDoc, ms);
                pdfDoc.Open();

                pdfDoc.Add(new Paragraph("Lista de Citas"));
                pdfDoc.Add(new Paragraph(" ")); // Espacio entre el título y la tabla

                PdfPTable table = new PdfPTable(6); // 6 columnas para los datos de citas
                table.AddCell("ID");
                table.AddCell("Nombre Completo");
                table.AddCell("Correo Electrónico");
                table.AddCell("Número de Contacto");
                table.AddCell("Servicios");
                table.AddCell("Fecha de Registro");

                foreach (var cita in citas)
                {
                    table.AddCell(cita.Id.ToString());
                    table.AddCell(cita.NombreCompleto);
                    table.AddCell(cita.CorreoElectronico);
                    table.AddCell(cita.NumeroContacto);
                    table.AddCell(cita.Servicios);
                    table.AddCell(cita.FechaRegistro?.ToString("MM/dd/yyyy") ?? "");
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                return File(ms.ToArray(), "application/pdf", "Citas.pdf");
            }
        }


        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.Id == id);
        }
    }
}
