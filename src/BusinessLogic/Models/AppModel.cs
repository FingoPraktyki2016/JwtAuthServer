using System;

namespace LegnicaIT.BusinessLogic.Models
{
    public class AppModel : BaseModel
    {
        public int AppModelId { get; set; }
        public String Name { get; set; }

        public override bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name));
        }
    }
}
