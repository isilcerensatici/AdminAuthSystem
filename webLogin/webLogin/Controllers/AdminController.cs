using Microsoft.AspNetCore.Mvc;

namespace webLogin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            if (Request.Cookies["AdminAccess"] != "true") // Sadece isilcerensatici giriş yaptıysa izin ver
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        
    }
}