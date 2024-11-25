using APWG1.Architecture;
using Microsoft.AspNetCore.Authentication.Cookies;
using PAWG1.Mvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la autenticaci�n y autorizaci�n
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

// Configuraci�n de la tuber�a de solicitudes
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es de 30 d�as. Puedes cambiar esto para escenarios de producci�n.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Usar la autenticaci�n
app.UseAuthorization();  // Usar la autorizaci�n

// Configuraci�n de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
