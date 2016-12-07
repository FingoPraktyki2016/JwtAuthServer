using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegnicaIT.DataAccess.Models
{
    public class App : BaseEntity
    {
        [Column(TypeName = "NVCHAR")]
        [StringLength(150)]
        [Required]
        public String Name { set; get; }

    }
}
