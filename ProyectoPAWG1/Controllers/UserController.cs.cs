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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Email,Password,State")] CMP.User user)

          
        {

            if (ModelState.IsValid)
            {
         

                var found = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/UserApi/save", JsonProvider.Serialize(user));
                return (found != null)
                    ? RedirectToAction(nameof(Index))
                    : View(user);
            }

            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/{id}", $"{id}");
            if (user == null)
                return NotFound();

            return View(JsonProvider.DeserializeSimple<User>(user));
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUser,Username,Email,Password,State")] CMP.User user)
        {
            if (user == null || id != user.IdUser)
                return NotFound();

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

    }
}
