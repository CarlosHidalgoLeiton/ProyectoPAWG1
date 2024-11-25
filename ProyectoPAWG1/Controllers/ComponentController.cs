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
    public class ComponentController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {

        private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;

        [HttpGet]
        public async Task<IActionResult> Index() {

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/all", null);

            var components = JsonProvider.DeserializeSimple<IEnumerable<CMP.Component>>(data);

            return View(components);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeRefresh,TypeComponent,Size,ApiUrl,ApiKey,ApiKeyId,Descrip,Title,Color,State,AllowedRole")] CMP.Component component, IFormFile Simbol)
        {
            ModelState.Remove("Simbol");

            component.IdOwner = 1;

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

        [HttpGet]
        public async Task<IActionResult> Details(int id) 
        {
            if (id == null)
                return NotFound();

            var component = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}");

            if (component == null)
                return NotFound();
            
            return View(JsonProvider.DeserializeSimple<CMP.Component>(component));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            if (id == null)
                return NotFound();

            var component = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}");

            if (component == null)
                return NotFound();

            return View(JsonProvider.DeserializeSimple<CMP.Component>(component));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("IdComponent,IdOwner,TimeRefresh,TypeComponent,Size,ApiUrl,ApiKey,ApiKeyId,Descrip,Title,Color,State,AllowedRole")] CMP.Component component, IFormFile Simbol)
        {
            if (id == null)
                return NotFound();

            ModelState.Remove("Simbol");

            CMP.Component updated = default;
            if (ModelState.IsValid)
            {
                try
                {

                    if (Simbol != null && Simbol.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await Simbol.CopyToAsync(memoryStream);
                            component.Simbol = memoryStream.ToArray();
                        }
                    }
                    else 
                    {
                        var get = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}");
                        var getComponent = JsonProvider.DeserializeSimple<CMP.Component>(get);

                        if (getComponent == null)
                            return NotFound();
                        component.Simbol = getComponent.Simbol;
                    }

                    var found = await _restProvider.PutAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}", JsonProvider.Serialize(component));

                    if (found == null)
                        return NotFound();

                    updated = JsonProvider.DeserializeSimple<CMP.Component>(found);
                }
                catch (DbUpdateConcurrencyException ducex) {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(updated);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        { 
            if (id == null)
                return NotFound();

            var component = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}");

            if (component == null)
                return NotFound();

            return View(JsonProvider.DeserializeSimple<CMP.Component>(component));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
                return NotFound();

            var component = await _restProvider.DeleteAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}");

            return (component == null) ? NotFound() : RedirectToAction(nameof(Index));        
        }
    }
}
