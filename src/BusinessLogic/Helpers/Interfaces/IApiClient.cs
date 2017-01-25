using System.Net.Http;
using LegnicaIT.BusinessLogic.Models.Common;

namespace LegnicaIT.BusinessLogic.Helpers.Interfaces
{
    public interface IApiClient
    {
        void Initialize();

        void AddHeader(string key, string value);

        void AddParameter(string key, string value);

        FormUrlEncodedContent GetCallContent();

        string GetCallRoute(string route);

        string GetCallRouteWithParameters(string route);

        ApiResponseModel MakeCallPost(string route);

        ApiResponseModel MakeCallGet(string route);
    }
}