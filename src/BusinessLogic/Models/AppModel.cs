namespace LegnicaIT.BusinessLogic.Models
{
    public class AppModel : BaseModel
    {
        public int AppModelId { get; set; }
        public string Name { get; set; }

        public override bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name));
        }
    }
}
