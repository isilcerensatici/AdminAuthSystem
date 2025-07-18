using System;
using System.ComponentModel.DataAnnotations;

namespace webLogin.Models
{
    public class UserModel
    {
        [Key]
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(4)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;//kullanıcı kayıt zamanı
    }
}
