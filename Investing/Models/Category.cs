using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Investing.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        [Column("id_category")]
        public long IdCategory { get; set; }

        [Required]
        [DisplayName("Наименование категории")]
        [Column("name_category")]
        public string NameCategory { get; set; }

        //[DisplayName("Очередь показа")]
        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Необходимо выше нуля")]
        //public int DisplayOrder { get; set; }

    }
}
