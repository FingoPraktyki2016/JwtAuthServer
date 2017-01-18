using LegnicaIT.BusinessLogic.Models.Base;
using System;

namespace LegnicaIT.BusinessLogic.Models.App
{
    public class AppModel : BaseModel
    {
        public int AppModelId { get; set; }
        public String Name { get; set; }
    }
}