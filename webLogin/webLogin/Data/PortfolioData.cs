using webLogin.Models;

namespace webLogin.Data
{
    public class PortfolioData
    {
        // Uygulama çalıştığında başlangıç verilerini yükleyen metot
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Eğer About tablosu boşsa — ilk kayıt eklenir
            if (!context.About.Any())
            {
                context.About.Add(new About
                {
                    FullName = "Işıl Ceren Satıcı", // Ad Soyad
                    Title = "Bilgisayar Mühendisi", // Mesleki unvan
                    Bio = "Yapay zekâ, web uygulamaları ve arayüz tasarımlarında pratik çözümler geliştirmeyi seven bir yazılım geliştirici.",
                    ProfileImageFileName = "8ics.jpg" // Görsel dosya adı (wwwroot/img/profile içinde olmalı)
                });

                context.SaveChanges();
            }
        }
    }}

