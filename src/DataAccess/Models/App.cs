using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegnicaIT.DataAccess.Models
{
    public class App
    {
        [Key]
        [Required]
        public int Id { set; get; }

        [Column(TypeName = "NVCHAR")]
        [StringLength(150)]
        [Required]
        public String Name { set; get; }
    }
}
