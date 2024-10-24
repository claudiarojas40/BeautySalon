using BeautySalon.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Controllers
{
    public class AppointmentController : Controller
    {
        private static List<Appointment> appointments = new List<Appointment>();

        // Método para mostrar el formulario de agendamiento de citas
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Método para manejar el guardado de la cita
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Aquí podrías guardar la cita en una base de datos.
                appointments.Add(appointment);
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // Método para mostrar todas las citas
        public IActionResult Index()
        {
            return View(appointments);
        }

        // Método para descargar las citas como CSV
        public IActionResult Download()
        {
            var csv = "Id,Name,Email,Service,AppointmentDate\n";
            foreach (var appointment in appointments)
            {
                csv += $"{appointment.Id},{appointment.Name},{appointment.Email},{appointment.Service},{appointment.AppointmentDate}\n";
            }

            var byteArray = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(byteArray, "text/csv", "appointments.csv");
        }
    }
}

        
        

    