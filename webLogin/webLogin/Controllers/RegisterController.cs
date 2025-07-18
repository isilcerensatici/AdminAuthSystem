using Microsoft.AspNetCore.Mvc;
using webLogin.Data;
using webLogin.Models;
using System.Linq;

namespace webLogin.wwwroot.lib.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppDbContext _context;

        public RegisterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model) // RegisterViewModel sınıfından gelen verileri alıyoruz.
        {
            if (!ModelState.IsValid) // Validation kontrolü yapılıyor. Girilen değerler geçerli mi değil mi kontrol ediliyor.
            {
                return View(model);
            }

            if (model.BirthDate == null)
            {
                ViewBag.RegisterError = "Doğum tarihi zorunludur.";
                return View(model);
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Username == model.Username);
            // Kullanıcı adını veritabanında ara.
            if (existingUser != null)
            {
                ViewBag.RegisterError = "Bu kullanıcı adı zaten mevcut!";
                return View(model);
            }

            var newUser = new UserModel // Yeni kullanıcı kaydı
            {
                Username = model.Username,
                Password = model.Password,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                BirthDate = model.BirthDate.Value,
                CreatedDate = DateTime.Now // Yeni eklenen alan kayıt tarihi.sistem tarafınadn otomatik olarak atanıyor.
            };

            _context.Users.Add(newUser); // Veritabanına ekleme
            _context.SaveChanges();      // Değişiklikleri kaydetme

            ViewBag.RegisterSuccess = "Kayıt başarıyla tamamlandı.";
            ModelState.Clear();
            return View();
        }

        public IActionResult MemberList()
        {
            var members = _context.Users.ToList();  // Veritabanındaki tüm kullanıcıları al
            return View(members);                   // View'a gönder
        }
    }
}
