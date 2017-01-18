//using System;
//using System.Linq;
//using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
//using LegnicaIT.DataAccess.Repositories.Interfaces;

//namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
//{
//    public class RevokeRole : IRevokeRole
//    {
//        private readonly IUserRoleRepository userRoleRepository;
//        private readonly IRoleRepository roleRepository;

//        public RevokeRole(IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
//        {
//            this.userRoleRepository = userRoleRepository;
//            this.roleRepository = roleRepository;
//        }

//        public void Invoke(int appId, int user)
//        {
//            var userRole = userRoleRepository.GetAll().FirstOrDefault(m => m.User.Id == user && m.App.Id == appId);

//            try
//            {
//                var changeRole = roleRepository.GetById(userRole.Role.Id + 1);

//                // Don't change SuperAdmin role
//                if (changeRole == null || changeRole.Id == 2)
//                {
//                    return;
//                }

//                userRole.Role = changeRole;

//                userRoleRepository.Edit(userRole);
//                userRoleRepository.Save();
//            }
//            catch (NullReferenceException e)
//            {
//                Console.WriteLine(e);
//            }
//        }
//    }
//}