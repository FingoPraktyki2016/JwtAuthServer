using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegnicaIT.DataAccess.Models
{
    public class User : BaseEntity
    {
        [MaxLength]
        [Column(TypeName = "ntext")]
        [Required]
        public String Email { set; get; }

        [MaxLength]
        [Column(TypeName = "ntext")]
        [Required]
        public String Password { set; get; }

        public DateTime EmailConfirmedOn { set; get; }

        [MaxLength]
        [Column(TypeName = "ntext")]
        [StringLength(100)]
        public String Name { set; get; }

        public DateTime LockedOn { set; get; }

    }
}
