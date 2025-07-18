using System.ComponentModel.DataAnnotations;

namespace webLogin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gerekli")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Şifre gerekli")]
        [MinLength(4, ErrorMessage = "Şifre en az 4 karakter olmalı")]
        public string? Password { get; set; }
    }
}
