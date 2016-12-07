using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegnicaIT.DataAccess.Models
{
    public class App : BaseEntity
    {
        [MaxLength]
        [Column(TypeName = "ntext")]
        [Required]
        public String Name { set; get; }

    }
}
