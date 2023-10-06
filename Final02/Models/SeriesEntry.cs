using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final02.Models
{
    public partial class SeriesEntry
    {
        [Key]
        [Column("Id")]
        public int SeriesEntry_Id { get; set; }
        public int? PlayerId { get; set; }
        public int FormatId { get; set; }

        public virtual Format? Format { get; set; }
        public virtual Player? Player { get; set; }
    }
}
