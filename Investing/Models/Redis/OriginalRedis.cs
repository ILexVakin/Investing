using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Investing.Models.Redis
{
    [Table("original_redis")]
    public class OriginalRedis
    {
        [Key]
        [Column("id_original")]
        public int Id_original { get; set; }

        [Column("isin_original")]
        public string Isin_original { get; set; }
    }
}
