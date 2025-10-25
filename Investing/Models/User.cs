using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace Investing.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("first_name")]
        [DisplayName("Фамилия")]
        public string FirstName { get; set; }

        [Required]
        [Column("name")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Column("role_id")]
        public int Role_id { get; set; }

        [Required]
        [Column("age")]
        [DisplayName("Возраст")]
        public int Age { get; set; }

        //[Required]
        [Column("sex")]
        [DisplayName("Пол")]
        public string Sex { get; set; }
        public virtual Credentials Credentials { get; set; }
        public virtual UserRole UserRole { get; set; }

    }
}
