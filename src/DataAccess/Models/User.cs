using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegnicaIT.DataAccess.Models
{
    public class User : BaseEntity
    {
        [Column(TypeName = "NVARCHAR(256)")]       
        [Required]
        public String Email { set; get; }

        [Column(TypeName = "NVARCHAR(256)")]
        [Required]
        public String Password { set; get; }

        public DateTime EmailConfirmedOn { set; get; }

        [Column(TypeName = "NVARCHAR(100)")]
        [StringLength(150)]
        public String Name { set; get; }

        public DateTime LockedOn { set; get; }

    }
}
