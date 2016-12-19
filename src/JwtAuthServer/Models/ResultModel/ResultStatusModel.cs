using LegnicaIT.JwtAuthServer.GenericResult;

namespace LegnicaIT.JwtAuthServer.Models
{
    public class ResultStatusModel
    {
        public ResultCode Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}