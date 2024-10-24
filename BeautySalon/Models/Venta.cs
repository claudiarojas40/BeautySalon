using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Models;

public partial class Venta
{
    [Key]
    public int Id { get; set; }

    [Unicode(false)]
    public string? VentasExentas { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Descuento { get; set; }

    [Column("IVA", TypeName = "decimal(18, 0)")]
    public decimal? Iva { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Total { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal TotalPagar { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Codigo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    public int? IdUsuario { get; set; }

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("Venta")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
