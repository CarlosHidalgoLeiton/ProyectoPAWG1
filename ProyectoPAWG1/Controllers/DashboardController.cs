using APWG1.Architecture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PAWG1.Architecture.Providers;
using PAWG1.Mvc.Models;
using CMP = PAWG1.Models.EFModels;

namespace ProyectoPAWG1.Controllers
{
    public class DashboardController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {

        private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadData() 
        {
            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/all", null);

            var components = JsonProvider.DeserializeSimple<IEnumerable<CMP.Component>>(data);

            foreach (var component in components ?? Enumerable.Empty<CMP.Component>())
            {
                var url = $"{component.ApiUrl}{component.ApiKeyId}{component.ApiKey}";

                try
                {
                    var getInfo = await _restProvider.GetAsync(url, null);
                    component.Data = getInfo;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"It had happened an error trying to get the api information. {e}");
                }

            }

            return Json(components);
        }

        [HttpPost]
        public async Task<IActionResult> SaveFavorite(int id)
        {   

            var component = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}");

            var componenT = JsonProvider.DeserializeSimple<CMP.Component>(component);

            var favorite = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/ComponentApi/favorite", JsonProvider.Serialize(componenT));

            var result = JsonProvider.DeserializeSimple<CMP.Component>(favorite);

            return RedirectToAction(nameof(Index));
        }

    }
}
