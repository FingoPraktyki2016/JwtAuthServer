using LegnicaIT.BusinessLogic.Actions.Role.Interfaces;
using LegnicaIT.BusinessLogic.Models.Role;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.Role.Implementation
{
    public class EditRole : IEditRole
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUserAppRepository userAppRepository;
        private readonly IUserRepository userRepository;
        private readonly IAppRepository appRepository;

        public EditRole(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public void Invoke(RoleModel model)
        {
            var role = roleRepository.GetById(model.Id);
            if (role != null)
            {
                role.Name = model.Name;

                roleRepository.Edit(role);
                roleRepository.Save();
            }
        }
    }
}