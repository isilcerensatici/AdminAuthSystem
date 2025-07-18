using Microsoft.AspNetCore.Mvc;
using webLogin.Data;
using webLogin.Models;
using System.IO;

namespace webLogin.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;

        public PortfolioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Portfolyo sayfası
        public IActionResult Index()
        {
            var about = _context.About.FirstOrDefault();

            var galleryImages = _context.Gallery
                .Where(g => g.IsActive && g.ImageFileName != about.ProfileImageFileName)
                .Select(g => g.ImageFileName)
                .ToList();

            ViewBag.GalleryImages = galleryImages;
            return View(about);
        }






        // POST: Yeni dosya yüklenirse işle
        [HttpPost]
        public IActionResult Index(About model)
        {
            var about = _context.About.FirstOrDefault();
            if (about == null)
                return NotFound();

            if (model.ProfileImageFile != null)
            {
                // Eski dosyayı sil
                if (!string.IsNullOrEmpty(about.ProfileImageFileName))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/profile", about.ProfileImageFileName);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                // Yeni dosyayı GUID ile kaydet
                var ext = Path.GetExtension(model.ProfileImageFile.FileName);
                var newFileName = Guid.NewGuid().ToString() + ext;
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/profile", newFileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    model.ProfileImageFile.CopyTo(stream);
                }

                about.ProfileImageFileName = newFileName;
                _context.SaveChanges();

                TempData["message"] = "Yeni görsel yüklendi ve GUID ile kaydedildi.";
            }

            return RedirectToAction("Index");
        }

        // 🔄 Fiziksel olarak eklenen dosyayı GUID ile yeniden adlandır
        [HttpPost]
        public IActionResult RenameExistingImageAsGuid(string existingFileName = "8ics.jpg")
        {
            var about = _context.About.FirstOrDefault();
            if (about == null)
                return NotFound();

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/profile");
            var oldPath = Path.Combine(folder, existingFileName);

            if (System.IO.File.Exists(oldPath))
            {
                var ext = Path.GetExtension(existingFileName);
                var newFileName = Guid.NewGuid().ToString() + ext;
                var newPath = Path.Combine(folder, newFileName);

                System.IO.File.Move(oldPath, newPath);

                about.ProfileImageFileName = newFileName;
                _context.SaveChanges();

                TempData["message"] = "Mevcut görsel başarıyla GUID ile yeniden adlandırıldı.";
            }
            else
            {
                TempData["message"] = "Belirtilen dosya bulunamadı.";
            }

            return RedirectToAction("Index");

        }
    }
}

