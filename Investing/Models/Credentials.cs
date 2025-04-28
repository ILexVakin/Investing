using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace Investing.Models
{
    [Table("credentials")]
    public class Credentials
    {
        [Key]
        [Column("credential_id")]
        public long CredentialId { get; set; }

        [Required]
        [Column("user_id")]
        public long UserId { get; set; }

        [Required]
        [Column("login")]
        [DisplayName("Логин")]
        public string Login { get; set; }

        [Required]
        [Column("password")]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Column("is_remember_me")]
        [DisplayName("Запомнить меня")]
        public bool IsRememberMe { get; set; }

        //[Required]
        [Column("created_ats")]
        [DisplayName("Время создания")]
        public DateTime? CreatedAt { get; set; }

        public User? User { get; set; }  
    }
}
