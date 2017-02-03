using System.Collections.Generic;
using LegnicaIT.JwtManager.Models;

namespace LegnicaIT.JwtManager.Helpers
{
    public class BreadcrumbHelper
    {
        private readonly List<BreadcrumbModel> BreadcrumbItems = new List<BreadcrumbModel>();

        public BreadcrumbHelper()
        {
            BreadcrumbItems.Add(new BreadcrumbModel
            {
                Title = "Home",
                Action = "Index",
                Controller = "Home"
            });
        }

        public List<BreadcrumbModel> GetBreadcrumbItems()
        {
            return BreadcrumbItems;
        }

        public void Add(string title, string action, string controller)
        {
            BreadcrumbItems.Add(new BreadcrumbModel
            {
                Title = title,
                Action = action,
                Controller = controller
            });
        }
    }
}
