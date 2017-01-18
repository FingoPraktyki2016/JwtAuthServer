using LegnicaIT.BusinessLogic.Actions.Role.Interfaces;
using LegnicaIT.BusinessLogic.Models.Role;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.Role.Implementation
{
    public class AddNewRole : IAddNewRole
    {
        private readonly IRoleRepository roleRepository;

        public AddNewRole(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public void Invoke(RoleModel role)
        {
            var newRole = new DataAccess.Models.Role()
            {
                Id = role.Id,
                Name = role.Name
            };

            roleRepository.Add(newRole);
            roleRepository.Save();
        }
    }
}
