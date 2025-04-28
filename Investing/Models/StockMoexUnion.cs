using System;
using System.Buffers.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Investing.Models
{
    [Table("stock_moex")]
    public class StockMoexUnion
    {
        [Key]
        [Column("stock_id")]
        public long StockId { get; set; }

        [Column("secid")]
        public string? SecId { get; set; }

        [Column("pervprice")]
        public decimal Pervprice { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("short_name")]
        public string? ShortName { get; set; }

        [Column("image")]
        public byte[]? Image { get; set; }
    }
}
