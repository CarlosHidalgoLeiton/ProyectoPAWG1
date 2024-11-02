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

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/all", null);

            var components = JsonProvider.DeserializeSimple<IEnumerable<CMP.Component>>(data);

            return View(components);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeRefresh,TypeComponent,Size,ApiUrl,ApiKey,Descrip,Title,Color")] CMP.Component component, IFormFile Simbol)
        {
            ModelState.Remove("Simbol");

            if (ModelState.IsValid)
            {
                if (Simbol != null && Simbol.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Simbol.CopyToAsync(memoryStream);
                        component.Simbol = memoryStream.ToArray();
                    }
                }

                var found = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/ComponentApi/save", JsonProvider.Serialize(component));
                return (found != null)
                    ? RedirectToAction(nameof(Index))
                    : View(component);
            }

            return View(component);
        }
    }
}
