using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Models;

public partial class DetalleVenta
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Precio { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Suma { get; set; }

    public int? IdProducto { get; set; }

    public int? IdVenta { get; set; }

    public int Cantidad { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleVenta")]
    public virtual Producto? IdProductoNavigation { get; set; }

    [ForeignKey("IdVenta")]
    [InverseProperty("DetalleVenta")]
    public virtual Venta? IdVentaNavigation { get; set; }
}
