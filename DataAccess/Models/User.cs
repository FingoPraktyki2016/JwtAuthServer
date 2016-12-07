using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegnicaIT.DataAccess.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { set; get; }

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

        public DateTime CreatedOn { set; get; }

        public DateTime ModifiedOn { set; get; }

        public DateTime DeletedOn { set; get; }
    }
}
