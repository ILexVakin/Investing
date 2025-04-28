using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Investing.Models
{
    [Table("user_role")]
    public class UserRole
    {
        [Key]
        [Column("user_role_id")]
        public int UserRoleId {  get; set; }

        [Required]
        [Column("role_name")]
        [DisplayName("Роль пользователя")]
        public string RoleName { get; set; }
    }
}
