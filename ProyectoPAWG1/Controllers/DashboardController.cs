using APWG1.Architecture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using PAWG1.Architecture.Helpers;
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
        public IActionResult Index()
        {
            ViewBag.Page = "Dashboard Page";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadData()
        {
            
            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/dashboard", null);
            var components = JsonProvider.DeserializeSimple<IEnumerable<CMP.Component>>(data);
            var userRole = UserHelper.GetUserRole(User);
            var userId = UserHelper.GetAuthenticatedUserId(User);
            components = components.Where(c =>
                c.AllowedRole == "All" || 
                (c.AllowedRole == "Admin" && userRole == "1") || 
                (c.AllowedRole == "User" && userRole == "2")  
            ).ToList();

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
                    Console.WriteLine($"Ocurrió un error al intentar obtener la información de la API: {e}");
                }
            }


            return Json(new { userId, components });
        }



        [HttpPost]
        public async Task<IActionResult> SaveStatus(int id, string type)
        {
            var userIdClaim = UserHelper.GetAuthenticatedUserId(User);
            Status status = new Status() 
            { 
                ComponentId = id,
                UserId = userIdClaim.Value,
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
            var userIdClaim = UserHelper.GetAuthenticatedUserId(User);
            //Cambiar el usuario que esta quemado
            var status = statuses.FirstOrDefault(x => (x.ComponentId == id) && (x.UserId == userIdClaim.Value));

            var deleted = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/StatusApi/deleteStatus", JsonProvider.Serialize(status));

            return RedirectToAction(nameof(Index));
        }

    }
}
