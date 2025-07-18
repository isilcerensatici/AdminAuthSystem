using Microsoft.AspNetCore.Mvc;
using webLogin.Data;

namespace webLogin.wwwroot.lib.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;// AppDbContext sınıfından bir örnek oluşturuyoruz. Bu sınıf veritabanı işlemlerini yapmamızı sağlar.

        public UserController(AppDbContext context)// Dependency Injection ile AppDbContext sınıfını alıyoruz.  
        {
            _context = context;// AppDbContext sınıfının örneğini _context değişkenine atıyoruz.
        }

        public IActionResult MemberList()
        {
            var members = _context.Users.ToList();//veirabanındaki tüm kullanıcıları çekiyoruz.
            return View(members);// Kullanıcı listesini View'a gönderiyoruz.
        }
    }
}
