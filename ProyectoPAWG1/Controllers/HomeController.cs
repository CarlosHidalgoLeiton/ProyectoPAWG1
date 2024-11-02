using Microsoft.AspNetCore.Mvc;
using PAWG1.Architecture.Providers;
using PAWG1.Models;
using System.Diagnostics;
using PAWG1.Models.EFModels;
using APWG1.Architecture;
using Microsoft.Extensions.Options;
using PAWG1.Mvc.Models;

namespace ProyectoPAWG1.Controllers
{
    public class HomeController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {

        private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComponent,TimeRefresh,TypeComponent,Size,ApiUrl,ApiKey,UserId,CreateDate,UpdateDate,Descrip,Title,Color,Simbol")] Component component)
        {
            if (ModelState.IsValid)
            {
                var found = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/ProductApi/save", JsonProvider.Serialize(component));
                return (found != null)
                    ? RedirectToAction(nameof(Index))
                    : View(component);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
