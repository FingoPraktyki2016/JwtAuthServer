using System;

namespace LegnicaIT.BusinessLogic.Models.Token
{
    public class VerifyResultModel
    {
        public bool IsValid { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
