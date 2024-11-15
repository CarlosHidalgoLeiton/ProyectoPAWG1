using APWG1.Architecture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Options;
using PAWG1.Architecture.Providers;
using PAWG1.Mvc.Models;
using CMP = PAWG1.Models.EFModels;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoPAWG1.Controllers
{
    public class LoginController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {
        private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;
        // GET: LoginController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Username,Email,Password,State,IdRole")] CMP.User user)
        {
            ModelState.Remove("IdRoleNavigation");
            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/all", null);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var existingUsers = JsonSerializer.Deserialize<List<CMP.User>>(data, options);

            // Validación del Username
            if (existingUsers != null && existingUsers.Any(existingUser => existingUser.Username == user.Username))
            {
                ModelState.AddModelError("Username", "The username already exists. Please choose another one.");
                return View(user);
            }

            // Encriptar la contraseña
            user.Password = HashPassword(user.Password);

            if (ModelState.IsValid)
            {
                var jsonUser = JsonSerializer.Serialize(user);
                var found = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/UserApi/save", jsonUser);

                return (found != null)
                    ? RedirectToAction(nameof(Index))
                    : View(user);
            }

            return View(user);
        }
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }


    }
}
