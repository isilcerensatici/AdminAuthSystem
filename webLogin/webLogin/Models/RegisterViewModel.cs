using System;
using System.ComponentModel.DataAnnotations;

namespace webLogin.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        [MinLength(4, ErrorMessage = "Şifre en az 4 karakter olmalı")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ad Soyad zorunludur")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cep telefonu zorunludur")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres zorunludur")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Doğum tarihi zorunludur")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; } // Burada nullable yaptık. DateTime yazarsak sistemde bir sıkıntı olabilir.
        // Datetime? ve required ile kontrollu bir şekilde hata mesajı yazdırıyoruz. kullanıcının boş geçmesine izin veriyoruz.
    }
}
