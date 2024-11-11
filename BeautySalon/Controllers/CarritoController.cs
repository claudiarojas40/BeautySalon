using BeautySalon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using BeautySalon;


public class CarritoController : Controller
{
    private const string SessionCarrito = "Carrito";

    // Acción para agregar un producto al carrito
    public IActionResult AgregarAlCarrito(int id)
    {
        // Simulación de obtener el producto por ID (deberías reemplazarlo con tu lógica de obtención de datos)
        var producto = ObtenerProductoPorId(id);
        if (producto == null)
        {
            return NotFound();
        }

        // Obtén el carrito de la sesión o crea uno nuevo
        var carrito = ObtenerCarritoDeSesion();

        // Verifica si el producto ya está en el carrito
        var itemExistente = carrito.FirstOrDefault(c => c.Producto.Id == id);
        if (itemExistente != null)
        {
            // Si el producto ya está, incrementa la cantidad
            itemExistente.Cantidad++;
        }
        else
        {
            // Si no está en el carrito, agrégalo como nuevo item
            carrito.Add(new CarritoItem { Producto = producto, Cantidad = 1 });
        }

        // Guarda el carrito actualizado en la sesión
        GuardarCarritoEnSesion(carrito);

        return RedirectToAction("Index"); // Redirige a la vista del carrito (ajusta según tu diseño)
    }

    // Acción para ver el contenido del carrito
    public IActionResult Index()
    {
        var carrito = ObtenerCarritoDeSesion();
        return View(carrito);
    }

    // Acción para eliminar un producto del carrito
    public IActionResult EliminarDelCarrito(int id)
    {
        var carrito = ObtenerCarritoDeSesion();
        var item = carrito.FirstOrDefault(c => c.Producto.Id == id);
        if (item != null)
        {
            carrito.Remove(item);
            GuardarCarritoEnSesion(carrito);
        }
        return RedirectToAction("Index");
    }

    // Métodos auxiliares
    private Producto ObtenerProductoPorId(int id)
    {
        // Simulación de datos
        var productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Shampoo", PrecioVenta = 15.0m },
            new Producto { Id = 2, Nombre = "Acondicionador", PrecioVenta = 12.0m },
            new Producto { Id = 3, Nombre = "Mascarilla", PrecioVenta = 20.0m }
        };

        return productos.FirstOrDefault(p => p.Id == id);
    }

    private List<CarritoItem> ObtenerCarritoDeSesion()
    {
        var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>(SessionCarrito);
        return carrito ?? new List<CarritoItem>();
    }

    private void GuardarCarritoEnSesion(List<CarritoItem> carrito)
    {
        HttpContext.Session.SetObjectAsJson(SessionCarrito, carrito);
    }
}
