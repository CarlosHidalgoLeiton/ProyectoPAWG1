using Microsoft.AspNetCore.Mvc;
using PAWG1.Architecture.Providers;
using PAWG1.Models;
using System.Diagnostics;
using PAWG1.Models.EFModels;
using APWG1.Architecture;
using Microsoft.Extensions.Options;
using PAWG1.Mvc.Models;
using Microsoft.EntityFrameworkCore;
using PAWG1.Architecture.Exceptions;

namespace ProyectoPAWG1.Controllers
{
    public class ComponentController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {

        private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComponent,TimeRefresh,TypeComponent,Size,ApiUrl,ApiKey,UserId,CreateDate,UpdateDate,Descrip,Title,Color,Simbol")] Component component)
        {
            if (ModelState.IsValid)
            {
                var found = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/ComponentApi/save", JsonProvider.Serialize(component));
                return (found != null)
                    ? RedirectToAction(nameof(Index))
                    : View(component);
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}");
            if (product == null)
                return NotFound();

            return View(JsonProvider.DeserializeSimple<Component>(product));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}");
            if (product == null)
                return NotFound();

            return View(JsonProvider.DeserializeSimple<Component>(product));
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComponent,TimeRefresh,TypeComponent,Size,ApiUrl,ApiKey,UserId,CreateDate,UpdateDate,Descrip,Title,Color,Simbol")] Component component)
        {
            if (component == null || id != component.IdComponent)
                return NotFound();

            Component? updated = default;
            if (ModelState.IsValid)
            {
                try
                {
                    var found = await _restProvider.PutAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}", JsonProvider.Serialize(component));
                    if (found == null)
                        return NotFound();

                    updated = await JsonProvider.DeserializeAsync<Component>(found);
                }
                catch (DbUpdateConcurrencyException ducex)
                {
                    return (!ComponentExists(component.IdComponent))
                        ? NotFound()
                        : throw PAWException.MustThrow(ducex);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(updated);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var component = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}");
            if (component == null)
                return NotFound();

            return View(JsonProvider.DeserializeSimple<Component>(component));
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var component = await _restProvider.DeleteAsync($"{_appSettings.Value.RestApi}/ComponentApi/{id}", $"{id}");
            return (component == null)
                ? NotFound()
                : RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        private bool ComponentExists(int id)
        {
            return true;
        }


    }
}
