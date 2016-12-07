using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegnicaIT.DataAccess.Models
{
    public class User : BaseEntity
    {
        [Column(TypeName = "NVCHAR")]
        [StringLength(256)]
        [Required]
        public String Email { set; get; }

        [Column(TypeName = "NVCHAR")]
        [StringLength(256)]
        [Required]
        public String Password { set; get; }

        public DateTime EmailConfirmedOn { set; get; }

        [Required]
        [Column(TypeName = "NVCHAR")]
        [StringLength(100)]
        public String Name { set; get; }

        public DateTime LockedOn { set; get; }

    }
}
