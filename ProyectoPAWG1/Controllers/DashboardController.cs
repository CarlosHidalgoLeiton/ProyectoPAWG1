using APWG1.Architecture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PAWG1.Architecture.Providers;
using PAWG1.Models.EFModels;
using PAWG1.Mvc.Models;
using CMP = PAWG1.Models.EFModels;

namespace ProyectoPAWG1.Controllers
{
    public class DashboardController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {

        private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var dataTime = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/TimeRefreshApi/1", null);

            var timeRefreshs = JsonProvider.DeserializeSimple<CMP.TimeRefresh>(dataTime);

            ViewBag.timeRefresh = timeRefreshs;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadData() 
        {
            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/dashboard", null);

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
        public async Task<IActionResult> SaveStatus(int id, string type)
        {   
            Status status = new Status() 
            { 
                ComponentId = id,
                UserId = 1,
                Type = type
            };

            var favorite = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/StatusApi/save", JsonProvider.Serialize(status));

            var result = JsonProvider.DeserializeSimple<CMP.Component>(favorite);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/StatusApi/all", null);

            var statuses = JsonProvider.DeserializeSimple<IEnumerable<Status>>(data);

            //Cambiar el usuario que esta quemado
            var status = statuses.FirstOrDefault(x => (x.ComponentId == id) && (x.UserId == 1));

            var deleted = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/StatusApi/deleteStatus", JsonProvider.Serialize(status));

            return RedirectToAction(nameof(Index));
        }

    }
}
