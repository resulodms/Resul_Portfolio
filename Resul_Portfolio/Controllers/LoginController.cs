using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Models;
using System.Security.Claims;

namespace Resul_Portfolio.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Index(string? returnUrl)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return Redirect("/Admin/Dashboard/Index");
            }

            return View(new LoginViewModel { ReturnUrl = returnUrl });

        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var username = _config["AdminUser:UserName"];
            var password = _config["AdminUser:Password"];

            if (model.UserName != username || model.Password != password)
            {
                ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = model.RememberMe });

            // Sadece kendi sitemizin içindeki adrese dön (güvenlik)
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return Redirect("/Admin/Dashboard/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }





    }







}


