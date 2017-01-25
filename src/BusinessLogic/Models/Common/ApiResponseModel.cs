using System.Net;

namespace LegnicaIT.BusinessLogic.Models.Common
{
    public class ApiResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}