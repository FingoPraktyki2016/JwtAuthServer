using System;
using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.BusinessLogic.Models.Base
{
    public class BaseModel
    {
        public int Id { set; get; }

        public DateTime CreatedOn { set; get; }

        public DateTime ModifiedOn { set; get; }

        public DateTime? DeletedOn { set; get; }
    }
}
