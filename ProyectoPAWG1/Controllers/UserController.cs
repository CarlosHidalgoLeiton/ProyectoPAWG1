using Microsoft.AspNetCore.Mvc;
using PAWG1.Architecture.Providers;
using CMP = PAWG1.Models.EFModels;
using APWG1.Architecture;
using Microsoft.Extensions.Options;
using PAWG1.Mvc.Models;
using PAWG1.Models.EFModels;
using PAWG1.Validator.Validators;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoPAWG1.Controllers
{
    public class UserController : Controller
    {
        private readonly IRestProvider _restProvider;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IValidatorUser _validatorUser;

        public UserController(IRestProvider restProvider, IOptions<AppSettings> appSettings, IValidatorUser validatorUser)
            : base() 
        {
            _restProvider = restProvider;
            _appSettings = appSettings;
            _validatorUser = validatorUser;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/all", null);

            var users = JsonProvider.DeserializeSimple<IEnumerable<CMP.User>>(data);

            return View(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/RoleApi/all", null);
            var roles = JsonProvider.DeserializeSimple<IEnumerable<CMP.Role>>(data);
            ViewBag.Roles = roles;
            if (TempData["ErrorUsername"] != null) { ModelState.AddModelError("Username", TempData["ErrorUsername"].ToString()); }
            if (TempData["ErrorEmail"] != null) { ModelState.AddModelError("Email", TempData["ErrorEmail"].ToString()); }
            if (TempData["ErrorPassword"] != null) { ModelState.AddModelError("Password", TempData["ErrorPassword"].ToString()); }
            if (TempData["ErrorRole"] != null) { ModelState.AddModelError("IdRole", TempData["ErrorRole"].ToString()); }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Email,Password,State,IdRole")] CMP.User user)
        {
            ModelState.Remove("IdRoleNavigation");
            bool? validationMessage = await _validatorUser.ValidatorCreate(user, TempData);

            if (validationMessage == false || validationMessage == null) { return RedirectToAction(nameof(Create)); }

            if (ModelState.IsValid)
            {
                var found = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/UserApi/save", JsonProvider.Serialize(user));
                return (found != null)
                    ? RedirectToAction(nameof(Index))
                    : View(user);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return RedirectToAction(nameof(Index)); }

            if (TempData["ErrorUsername"] != null) { ModelState.AddModelError("Username", TempData["ErrorUsername"].ToString()); }
            if (TempData["ErrorEmail"] != null) { ModelState.AddModelError("Email", TempData["ErrorEmail"].ToString()); }
            if (TempData["ErrorPassword"] != null) { ModelState.AddModelError("Password", TempData["ErrorPassword"].ToString()); }
            if (TempData["ErrorRole"] != null) { ModelState.AddModelError("IdRole", TempData["ErrorRole"].ToString()); }
            if (TempData["Password"] != null) { ModelState.Remove("Password"); }

            var user = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}");

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/RoleApi/all", null);

            var roles = JsonProvider.DeserializeSimple<IEnumerable<CMP.Role>>(data);

            ViewBag.Roles = roles;
            if (user == null)
                return NotFound();

            return View(JsonProvider.DeserializeSimple<User>(user));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUser,Username,Email,Password,State,IdRole")] CMP.User user)
        {
            if (user == null || id != user.IdUser) { return View(Index); }

            bool? ValidatorEdit = await _validatorUser.ValidatorEdit(id, user, TempData);

            if (ValidatorEdit == false || ValidatorEdit == null) { return RedirectToAction(nameof(Edit)); }
            ModelState.Remove("Password");

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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var users = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}");
            if (users == null)
                return RedirectToAction(nameof(Index));

            var user = JsonProvider.DeserializeSimple<User>(users);

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/RoleApi/{user.IdRole}", $"{user.IdRole}");
            if (data == null)
                return RedirectToAction(nameof(Index));

            var roles = JsonProvider.DeserializeSimple<CMP.Role>(data);

            ViewBag.Roles = roles;

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _restProvider.DeleteAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}");
            return (user == null)
                ? NotFound()
                : RedirectToAction(nameof(Index));
        }

    }
}
