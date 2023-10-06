using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Final02.Models
{
    public partial class Format
    {
        public Format()
        {
            SeriesEntries = new HashSet<SeriesEntry>();
        }

        public int FormatId { get; set; }
        [Required]
        [MaxLength(55)]
        [Column(TypeName = "varchar(55)")]
        public string? FormatName { get; set; }
        [NotMapped]
        public virtual ICollection<SeriesEntry> SeriesEntries { get; set; }
    }
}
