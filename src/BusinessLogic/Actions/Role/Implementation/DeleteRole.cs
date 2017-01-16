using LegnicaIT.BusinessLogic.Actions.Role.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.Role.Implementation
{
    public class DeleteRole : IDeleteRole
    {
        private readonly IRoleRepository roleRepository;

        public DeleteRole(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public void Invoke(int roleId)
        {
            var roleToDelete = roleRepository.GetById(roleId);
            if (roleToDelete != null)
            {
                roleRepository.Delete(roleToDelete);
                roleRepository.Save();
            }
        }
    }
}