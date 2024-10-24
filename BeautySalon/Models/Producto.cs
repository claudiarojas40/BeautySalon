using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Models;

public partial class Producto
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? PrecioUnitario { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? PrecioVenta { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? Codigo { get; set; }

    [Unicode(false)]
    public string? Imagen { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Marca { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaVencimiento { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Categoria { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
}
