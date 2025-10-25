using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace Investing.Models
{
    public class RegisterModel : User
    {

        [Key]
        [Column("credential_id")]
        public int CredentialId { get; set; }

        [Required]
        [Column("login")]
        [DisplayName("Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(8, ErrorMessage = "Пароль должен содержать не менее 8 символов.")]
        [Column("password")]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        public string ConfirmationCode { get; set; }
    }
}
