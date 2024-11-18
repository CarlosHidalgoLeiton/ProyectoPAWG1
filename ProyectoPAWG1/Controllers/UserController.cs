using Microsoft.AspNetCore.Mvc;
using PAWG1.Architecture.Providers;
using PAWG1.Models;
using System.Diagnostics;
using CMP = PAWG1.Models.EFModels;
using APWG1.Architecture;
using Microsoft.Extensions.Options;
using PAWG1.Mvc.Models;
using PAWG1.Architecture.Exceptions;
using PAWG1.Models.EFModels;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoPAWG1.Controllers
{
    public class UserController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {

       private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;


        [HttpGet]
        public async Task<IActionResult> Index() {

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/all", null);



            var users = JsonProvider.DeserializeSimple<IEnumerable<CMP.User>>(data);

            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/RoleApi/all", null);

            var roles = JsonProvider.DeserializeSimple<IEnumerable<CMP.Role>>(data);

            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Email,Password,State,IdRole")] CMP.User user)
        {
            ModelState.Remove("IdRoleNavigation");
            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/all", null);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var existingUsers = JsonSerializer.Deserialize<List<CMP.User>>(data, options);

            // Validación del Username
            if (existingUsers != null && existingUsers.Any(existingUser => existingUser.Username == user.Username))
            {
                ModelState.AddModelError("Username", "The username already exists. Please choose another one.");

                var dataRoles = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/RoleApi/all", null);

                var roles = JsonProvider.DeserializeSimple<IEnumerable<CMP.Role>>(dataRoles);

                ViewBag.Roles = roles;
                return View(user);
            }

            // Encriptar la contraseña
            user.Password = HashPassword(user.Password);

            //ModelState.IdRolenNavigation = ModelState.IdRole;
            if (ModelState.IsValid)
            {
         

                var found = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/UserApi/save", JsonProvider.Serialize(user));
                return (found != null)
                    ? RedirectToAction(nameof(Index))
                    : View(user); 
            }

            return View(Index);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}");

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/RoleApi/all", null);

            var roles = JsonProvider.DeserializeSimple<IEnumerable<CMP.Role>>(data);

            ViewBag.Roles = roles;
            if (user == null)
                return NotFound();

            return View(JsonProvider.DeserializeSimple<User>(user));
        }



        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}");

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/RoleApi/all", null);

            var roles = JsonProvider.DeserializeSimple<IEnumerable<CMP.Role>>(data);

            ViewBag.Roles = roles;
            if (user == null)
                return NotFound();

            return View(JsonProvider.DeserializeSimple<User>(user));
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUser,Username,Email,Password,State,IdRole")] CMP.User user)
        {
            if (user == null || id != user.IdUser)
                return NotFound();

            var dataUserId = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}");
            var userId = JsonProvider.DeserializeSimple<User>(dataUserId);

            if(userId.Username != user.Username)
            {
                var dataUsers = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/all", null);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var existingUsers = JsonSerializer.Deserialize<List<CMP.User>>(dataUsers, options);

                // Validación del Username
                if (existingUsers != null && existingUsers.Any(existingUser => existingUser.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "The username already exists. Please choose another one.");

                    var dataRoles = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/RoleApi/all", null);

                    var roles = JsonProvider.DeserializeSimple<IEnumerable<CMP.Role>>(dataRoles);

                    ViewBag.Roles = roles;
                    return View(user);
                }
            }

            if (user.Password == null)
            {
                var userg = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}");
                var getuser = JsonProvider.DeserializeSimple<User>(userg);

                user.Password = getuser.Password;
                ModelState.Remove("Password");
            }
            user.Password = HashPassword(user.Password);
            User? updated = default;
            if (ModelState.IsValid)
            {
                var found = await _restProvider.PutAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}", JsonProvider.Serialize(user));
                if (found == null)
                    return NotFound();

                updated = await JsonProvider.DeserializeAsync<User>(found);
                return RedirectToAction(nameof(Index));
            }
            return View(updated);
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

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var userS = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}");
            if (userS == null)
                return NotFound();

            var user = JsonProvider.DeserializeSimple<User>(userS);

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/RoleApi/{user.IdRole}", $"{user.IdRole}");
            if (data == null)
                return NotFound();
          
            var roles = JsonProvider.DeserializeSimple<CMP.Role>(data);

            ViewBag.Roles = roles;


            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _restProvider.DeleteAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}");
            return (user == null)
                ? NotFound()
                : RedirectToAction(nameof(Index));
        }



    }
}
