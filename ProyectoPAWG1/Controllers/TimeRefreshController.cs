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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProyectoPAWG1.Controllers
{
    //[Authorize]
    public class TimeRefreshController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {

        private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;


        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return RedirectToAction("Index", "Component");

        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeRefresh1")] CMP.TimeRefresh timeRefresh)
        {
            if (ModelState.IsValid)
            {
                if (timeRefresh.TimeRefresh1 > 0)
                {
                    timeRefresh.TimeRefreshId = 1;
                    var found = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/TimeRefreshApi/save", JsonProvider.Serialize(timeRefresh));
                    return (found != null) ? RedirectToAction("Index", "Component") : View(timeRefresh);
                }
            }
            return RedirectToAction("Index", "Component");
        }

    }
}
