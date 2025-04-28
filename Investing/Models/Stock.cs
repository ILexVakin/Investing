using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Investing.Models
{
    [Table("stock")]
    public class Stock
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        [DisplayName("Наименование акции")]
        public string Name { get; set; }

        [Required]
        [Column("short_description")]
        [DisplayName("Краткое описание акции")]
        public string ShortDesc { get; set; }

        [DisplayName("Описание акции")]
        [Column("description")]
        public string Description { get; set; }

        [DisplayName("Стоимость акции")]
        [Column("price")]
        [Range(1, int.MaxValue)]
        [Required]
        public double Price { get; set; }

        [DisplayName("Количество акций в лоте")]
        [Range(1, int.MaxValue)]
        [Column("lot_quantity")]
        [Required]
        public int LotQuantity { get; set; }

        [Column("category_id")]
        [Range(1, long.MaxValue)]
        [Required]
        public long CategoryId { get; set; }

        #region Устанавливаем связь между Продуктом и Категорией

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        #endregion

    }
}
