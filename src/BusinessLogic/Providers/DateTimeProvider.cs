using System;
using LegnicaIT.BusinessLogic.Providers.Interface;

namespace LegnicaIT.BusinessLogic.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetNow()
        {
            return DateTime.UtcNow;
        }
    }
}
