using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Investing.Models.Redis
{
    [Table("duplicate_redis")]
    public class DuplicateRedis
    {
        [Key]
        [Column("id_duplicate")]
        public int Id_duplicate {  get; set; }
        [Column("original_id")]
        public int Original_id { get; set; }

        [Column("isin_duplicate")]
        public string Isin_duplicate { get; set; }
    }
}
