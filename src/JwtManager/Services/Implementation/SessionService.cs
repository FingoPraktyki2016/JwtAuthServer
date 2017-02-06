using System;
using LegnicaIT.JwtManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LegnicaIT.JwtManager.Services.Implementation
{
    public class SessionService<T>:ISessionService<T>
    {
        private readonly string sessionKey;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession Session => httpContextAccessor.HttpContext.Session;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.sessionKey = typeof(T).Name;
        }

        public bool ContainsItem()
        {
            var item = GetItem();
            var defaultValue = default(T);
            if (item != null)
            {
                return defaultValue == null || !item.Equals(defaultValue);
            }
            return false;
        }

        public T GetItem()
        {
            var sessionItem = Session.GetString(sessionKey);
            if (string.IsNullOrEmpty(sessionItem))
                return default(T);
            try
            {
                var result = JsonConvert.DeserializeObject<T>(sessionItem);
                return result;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public bool AddItem(T item)
        {
            try
            {
                SaveItemToSession(sessionKey, item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Clear()
        {
            try
            {
                SaveItemToSession(sessionKey, default(T));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SaveItemToSession(string key, T item)
        {
            Session.SetString(key, JsonConvert.SerializeObject(item));
        }
    }
}