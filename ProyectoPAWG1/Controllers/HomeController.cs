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
    public class HomeController: Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
