using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Final02.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required]
        [MaxLength(55)]
        [Column(TypeName = "varchar(55)")]
        public string CourseName { get; set; }

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
