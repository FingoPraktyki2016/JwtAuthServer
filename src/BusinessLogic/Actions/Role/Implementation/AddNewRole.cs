using LegnicaIT.BusinessLogic.Actions.Role.Interfaces;
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

        public void Invoke(string roleName)
        {
            var newRole = new DataAccess.Models.Role()
            {
                Name = roleName
            };

            roleRepository.Add(newRole);
            roleRepository.Save();
        }
    }
}