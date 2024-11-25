using APWG1.Architecture;
using Microsoft.AspNetCore.Authentication.Cookies;
using PAWG1.Mvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la autenticación y autorización
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";  // Ruta al formulario de login
        options.AccessDeniedPath = "/Home/AccessDenied"; // Ruta de acceso denegado
    });

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IRestProvider, RestProvider>();
builder.Services.Configure<AppSettings>(builder.Configuration);

var app = builder.Build();

// Configuración de la tubería de solicitudes
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es de 30 días. Puedes cambiar esto para escenarios de producción.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Usar la autenticación
app.UseAuthorization();  // Usar la autorización

// Configuración de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
