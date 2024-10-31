using System;
using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Models
{
    public class Citas
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string CorreoElectronico { get; set; }

        [Required]
        [StringLength(250)]
        public string Servicios { get; set; }

        public DateOnly Fecha { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string NumeroContacto { get; set; }
    }
}
