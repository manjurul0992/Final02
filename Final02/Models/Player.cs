using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Final02.Models
{
    [Table("Players")]
    public partial class Player
    {
        public Player()
        {
            SeriesEntries = new HashSet<SeriesEntry>();
        }

        public int PlayerId { get; set; }
        [Required]
        [MaxLength(55)]
        [Column(TypeName = "varchar(55)")]
        public string PlayerName { get; set; } = null!;
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? DateOfBirth { get; set; }

        public string Phone { get; set; } = null!;
        public string? Picture { get; set; }
        public bool MaritalStatus { get; set; }

        public virtual ICollection<SeriesEntry> SeriesEntries { get; set; }
    }
}
