using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegnicaIT.DataAccess.Models
{
    public class App : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(150)")]
        public String Name { set; get; }
    }
}