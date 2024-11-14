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
                    
                }
               
                
            }

            return View(components);
        }

    }
}
