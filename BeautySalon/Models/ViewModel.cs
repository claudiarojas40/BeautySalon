namespace BeautySalon.Models
{
    public class DetalleVentaViewModel
    {
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public int Cantidad { get; set; }
        public int? IdProducto { get; set; }
    }

    public class VentaViewModel
    {
        public DateTime Fecha { get; set; }
        public string NombreCliente { get; set; }
        public string DireccionCliente { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPagar { get; set; }
        public List<DetalleVentaViewModel> DetalleVenta { get; set; }
    }


}
