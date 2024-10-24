using BeautySalon.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BDContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configurar la autenticaci�n con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login"; // Ruta para la p�gina de inicio de sesi�n
        options.LogoutPath = "/Logout"; // Ruta para el logout (se usa el controlador que creaste)
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Aqu� cambias la ruta por defecto al controlador y acci�n de citas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Appointment}/{action=Create}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
