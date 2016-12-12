using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegnicaIT.DataAccess.Models
{
    public class App : BaseEntity
    {
        [Column(TypeName = "NVARCHAR(100)")]
        [Required]
        public String Name { set; get; }

    }
}
