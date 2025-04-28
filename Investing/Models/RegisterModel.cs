using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace Investing.Models
{
    public class RegisterModel
    {

        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("first_name")]
        [DisplayName("Фамилия")]
        public string FirstName { get; set; }

        [Required]
        [Column("name")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required]
        [Column("age")]
        [DisplayName("Возраст")]
        public int Age { get; set; }

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
