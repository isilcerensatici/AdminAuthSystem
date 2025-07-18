using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

public class About
{
    public int Id { get; set; }                          // Her kullanıcının benzersiz kimliği
    public string? FullName { get; set; }               // Ad Soyad bilgisi
    public string? Title { get; set; }                  // Mesleki unvan veya rol
    public string? Bio { get; set; }                    // Kısa özgeçmiş açıklaması

    public string? ProfileImageFileName { get; set; }   // Yüklenen görselin dosya adı (örneğin: 2a0d.jpg gibi GUID’li)

    [NotMapped]
    public IFormFile? ProfileImageFile { get; set; }    // View’dan gelen dosya verisi (veritabanına kaydedilmez)
}

