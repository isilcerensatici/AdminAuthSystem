using Microsoft.AspNetCore.Mvc;
using webLogin.Models;
using webLogin.Data;
using System.Linq;

namespace webLogin.wwwroot.lib.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginData)
        {
            if (!ModelState.IsValid)//validation kontrolü yapılıyor.girilen değerler geçerli mi değil mi kontrol ediliyor.
            {
                // İlk hatayı al ve göster
                var errorMessage = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault();

                ViewBag.LoginError = errorMessage ?? "Bilinmeyen bir hata oluştu!";
                return View(loginData);
            }

            var userInDb = _context.Users.FirstOrDefault(u => u.Username == loginData.Username);
            //firstOrDefault metodu, veritabanında kullanıcı adını arar ve o kullanıcıya ait satırı getirir.
            // Eğer kullanıcı veritabanında yoksa userInDb null olur. Eğer varsa o kullanıcıya ait satırı getirir.

            if (userInDb == null)
            {
                ViewBag.LoginError = "Böyle bir kullanıcı bulunamadı!";
                return View(loginData);
            }

            if (userInDb.Password != loginData.Password)
            {
                ViewBag.LoginError = "Şifre yanlış!";
                return View(loginData);
            }

            // Giriş başarılı
            if (userInDb.Username.ToLower() == "isilcerensatici") //Cookie sadece bu kullanıcıya atanır
            {
                CookieOptions option = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(60)//60 dk süre veriyor. bu süre içinde seni tanıyor.
                };
                Response.Cookies.Append("AdminAccess", "true", option);

                return RedirectToAction("Dashboard", "Admin");//dashboard'a yönlendiriyor.
            }

            return RedirectToAction("Welcome", new { username = userInDb.Username });
        }

        public IActionResult Welcome(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        public IActionResult LogHandleAdminLogout()//admin çıkış işlemi.Tarayıcıdaki admin oturum cookie’si silinir yani admin yetkisi sona erer 

        {
            Response.Cookies.Delete("AdminAccess");
            return RedirectToAction("Login");
        }
    }
}