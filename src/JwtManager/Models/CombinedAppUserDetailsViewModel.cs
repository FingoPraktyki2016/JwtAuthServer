using System.Collections.Generic;

namespace LegnicaIT.JwtManager.Models
{
    public class CombinedAppUserDetailsViewModel
    {
        public FormModel<AppViewModel> App { get; set; }
        public List<UserDetailsFromAppViewModel> Users { get; set; }

        public CombinedAppUserDetailsViewModel(AppViewModel app)
        {
            App = new FormModel<AppViewModel>(app);
        }
    }
}
