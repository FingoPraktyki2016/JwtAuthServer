using System;

namespace LegnicaIT.BusinessLogic.Models.Token
{
    public class VerifyResultModel
    {
        public bool IsValid { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Email { get; set; }
        public int AppId { get; set; }
    }
}
