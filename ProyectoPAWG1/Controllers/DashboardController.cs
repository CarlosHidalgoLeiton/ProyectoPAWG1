using Microsoft.AspNetCore.Mvc;
using PAWG1.Architecture.Providers;
using PAWG1.Models;
using System.Diagnostics;
using CMP = PAWG1.Models.EFModels;
using APWG1.Architecture;
using Microsoft.Extensions.Options;
using PAWG1.Mvc.Models;
using Microsoft.EntityFrameworkCore;
using PAWG1.Architecture.Exceptions;

namespace ProyectoPAWG1.Controllers
{
    public class DashboardController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {

        private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;

        [HttpGet]
        public async Task<IActionResult> Index() {

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/all", null);

            var components = JsonProvider.DeserializeSimple<IEnumerable<CMP.Component>>(data);

            foreach (var c in components)
            {
                var getInfo = "";
                if (c.TypeComponent == "Weather")
                {
                    string url = c.ApiUrl + "&appid=" + c.ApiKey;
                    getInfo = await _restProvider.GetAsync(url, null);
                }
                else if (c.TypeComponent == "Exchange Rate") {
                    getInfo = await _restProvider.GetAsync(c.ApiUrl, null);
                }
                c.Data = getInfo;
            }

            return View(components);
        }

    }
}
