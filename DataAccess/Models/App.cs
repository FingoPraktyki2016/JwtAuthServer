using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
