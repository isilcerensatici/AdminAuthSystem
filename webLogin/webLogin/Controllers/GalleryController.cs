using Microsoft.AspNetCore.Mvc;
using webLogin.Data;
using webLogin.Models;
using System.IO;
using System;
using System.Linq;

namespace webLogin.Controllers
{
    public class GalleryController : Controller
    {
        private readonly AppDbContext _context;

        public GalleryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()//galeri görüntüleme
        {
            var images = _context.Gallery.ToList(); // Pasifleri de gösteriyorsan böyle kalsın
            return View(images);
        }

        [HttpPost]
        public IActionResult Index(Gallery model)// galeriye resim ekleme. guid olarak kaydeder.
        {
            if (model.ImageFile != null)//kullanıcı bir dosya seçmiş mi?
            {
                var ext = Path.GetExtension(model.ImageFile.FileName);//seçilen dosyanın uzantısı alınıyor.
                var newFileName = Guid.NewGuid().ToString() + ext;//dosyasının yeni adı oluşturuluyor. Guid ile benzersiz hale getiriliyor.
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/gallery", newFileName);//dosya nereye kaydedilsin

                using (var stream = new FileStream(savePath, FileMode.Create))// dosya kaydediliyor.
                {
                    model.ImageFile.CopyTo(stream);
                }

                model.ImageFileName = newFileName;// model'e yeni dosya adı atanıyor.
                model.IsActive = true;

                _context.Gallery.Add(model);// model veritabanına ekleniyor.
                _context.SaveChanges();
            }
            TempData["message"] = "Görsel başarıyla yüklendi!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id)//aktiflik durumu değiştirme
        {
            var image = _context.Gallery.FirstOrDefault(x => x.Id == id);// id'sine göre resim bulunuyor.
            if (image != null)// kontrol ediliyor ki resim var mı?
            {
                image.IsActive = !image.IsActive;// aktiflik durumu tersine çevriliyor.
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateImage(int id, IFormFile newImage)// resim güncelleme. eski dosya varsa siler.
        {
            var image = _context.Gallery.FirstOrDefault(x => x.Id == id);// id'sine göre resim bulunuyor.
            if (image == null || newImage == null)// kontrol ediliyor ki resim ve yeni dosya var mı?
                return RedirectToAction("Index");

            if (!string.IsNullOrEmpty(image.ImageFileName))// eski dosya adı varsa, eski dosyayı sileriz.
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/gallery", image.ImageFileName);// eski dosyanın yolu oluşturuluyor.
                if (System.IO.File.Exists(oldPath))// eski dosya var mı kontrol ediliyor.
                    System.IO.File.Delete(oldPath);// eski dosya siliniyor.
            }

            var ext = Path.GetExtension(newImage.FileName);// yeni dosyanın uzantısı alınıyor.
            var newFileName = Guid.NewGuid().ToString() + ext;// yeni dosya adı oluşturuluyor. Guid ile benzersiz hale getiriliyor.
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/gallery", newFileName);// yeni dosyanın kaydedileceği yol oluşturuluyor.

            using (var stream = new FileStream(savePath, FileMode.Create))// yeni dosya kaydediliyor.
            {
                newImage.CopyTo(stream);
            }

            image.ImageFileName = newFileName;// model'e yeni dosya adı atanıyor.
            _context.SaveChanges();
            TempData["message"] = "Görsel başarıyla güncellendi!";
            return RedirectToAction("Index");
        }
        public IActionResult AddManualGalleryImage(string fileName = "example.jpg")
        {
            var image = new Gallery
            {
                ImageFileName = fileName,
                IsActive = true
            };

            _context.Gallery.Add(image);
            _context.SaveChanges();

            TempData["message"] = "Manuel eklenen görsel galeriye başarıyla dahil edildi!";
            return RedirectToAction("Index"); // veya Gallery ekranı
        }

        [HttpPost]
        public IActionResult SetAsProfileImage(string fileName)
        {
            var about = _context.About.FirstOrDefault();
            if (about != null)
            {
                about.ProfileImageFileName = fileName;
                _context.SaveChanges();

                TempData["message"] = "Profil görseli güncellendi!";
            }

            return RedirectToAction("Index");
        }



    }
}

