using Microsoft.AspNetCore.Mvc;
using PAWG1.Architecture.Providers;
using PAWG1.Models;
using System.Diagnostics;
using CMP = PAWG1.Models.EFModels;
using APWG1.Architecture;
using Microsoft.Extensions.Options;
using PAWG1.Mvc.Models;
using PAWG1.Architecture.Exceptions;

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
        public async Task<IActionResult> Create([Bind("Username,Email,Size,Password,State")] CMP.User user, IFormFile Simbol)

          
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
    }
}
