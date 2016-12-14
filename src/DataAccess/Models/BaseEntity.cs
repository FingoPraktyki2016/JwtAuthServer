using System;
using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.DataAccess.Models
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public int Id { set; get; }

        public DateTime CreatedOn { set; get; }

        public DateTime ModifiedOn { set; get; }

        public DateTime DeletedOn { set; get; }
    }
}
