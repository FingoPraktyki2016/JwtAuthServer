using System;

namespace LegnicaIT.BusinessLogic.Models.Token
{
    public class VerifyEmailTokenResultModel
    {
        public bool IsValid { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
    }
}
