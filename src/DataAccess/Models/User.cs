using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegnicaIT.DataAccess.Models
{
    public class User : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        public String Email { set; get; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        public String PasswordHash { set; get; }

        [Required]
        [Column(TypeName = "NVARCHAR(128)")]
        public string PasswordSalt { set; get; }

        public DateTime EmailConfirmedOn { set; get; }

        [Column(TypeName = "NVARCHAR(100)")]
        public String Name { set; get; }

        public DateTime? LockedOn { set; get; }
    }
}