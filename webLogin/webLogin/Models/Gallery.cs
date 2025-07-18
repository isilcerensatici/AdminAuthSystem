using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webLogin.Models
{
    public class Gallery
    {
        public int Id { get; set; }

        [Required]
        public string? ImageFileName { get; set; } // Dosya adı örn: d3f13d...jpg

        public bool IsActive { get; set; } = true;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }//veritabanına hiç kaydedilmeyen, sadece formdan gelen geçici dosya bilgisini yakalamak için kullandığımız özel bir alan.



    }
}


