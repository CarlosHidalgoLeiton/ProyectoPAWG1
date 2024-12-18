using APWG1.Architecture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PAWG1.Mvc.Models;
using CMP = PAWG1.Models.EFModels;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using PAWG1.Validator.Validators;
using PAWG1.Architecture.Helpers;

namespace ProyectoPAWG1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRestProvider _restProvider;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IValidatorUser _validatorUser;

        public LoginController(IRestProvider restProvider, IOptions<AppSettings> appSettings, IValidatorUser validatorUser)
        {
            _restProvider = restProvider ?? throw new ArgumentNullException(nameof(restProvider));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _validatorUser = validatorUser ?? throw new ArgumentNullException(nameof(validatorUser));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Username,Email,Password,State,IdRole")] CMP.User user)
        {
            ModelState.Remove("IdRoleNavigation");

            bool? validationMessage = await _validatorUser.ValidatorCreate(user, TempData);
            if (validationMessage == false || validationMessage == null)
            {
                ViewBag.ErrorMessage = "Validation failed. Please check the input.";
                return View(user);
            }

            if (ModelState.IsValid)
            {
                var jsonUser = JsonSerializer.Serialize(user);
                var result = await _restProvider.PostAsync($"{_appSettings.Value.RestApi}/UserApi/save", jsonUser);

                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "An error occurred while saving the user.");
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] CMP.User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                ViewBag.ErrorMessage = "Username and password are required.";
                return View("Index");
            }

            string hashedPassword = UserHelper.HashPassword(user.Password);

            var data = await _restProvider.GetAsync($"{_appSettings.Value.RestApi}/UserApi/all", null);
            if (string.IsNullOrEmpty(data))
            {
                ViewBag.ErrorMessage = "No user data available.";
                return View("Index");
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var existingUsers = JsonSerializer.Deserialize<List<CMP.User>>(data, options);

            var matchedUser = existingUsers?.FirstOrDefault(u =>
                u.Username == user.Username && u.Password == hashedPassword);

            if (matchedUser != null)
            {
                if (!matchedUser.State)
                {
                    ViewBag.ErrorMessage = "Your account is inactive. Please contact support.";
                    return View("Index");
                }

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, matchedUser.Username),
            new Claim(ClaimTypes.Email, matchedUser.Email),
            new Claim(ClaimTypes.Role, matchedUser.IdRoleNavigation.Name),
            new Claim(ClaimTypes.NameIdentifier, matchedUser.IdUser.ToString())
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.ErrorMessage = "Incorrect credentials. Please try again.";
            return View("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }

     
    }
}