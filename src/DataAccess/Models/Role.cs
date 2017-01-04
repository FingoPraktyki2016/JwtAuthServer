using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegnicaIT.DataAccess.Models
{
    public class Role : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(150)")]
        public String Name { get; set; }
    }
}
