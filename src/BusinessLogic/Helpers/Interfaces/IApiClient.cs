using System.Net.Http;

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
        string MakeCallPost(string route);
        string MakeCallGet(string route);
    }
}
