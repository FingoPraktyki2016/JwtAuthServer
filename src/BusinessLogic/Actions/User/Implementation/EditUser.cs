using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class EditUser : IEditUser
    {
        private readonly IUserRepository userRepository;

        public EditUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Invoke(UserModel user)
        {
            var userToEdit = userRepository.GetById(user.Id);

            userToEdit.Name = user.Name;
            userToEdit.EmailConfirmedOn = user.EmailConfirmedOn;
            userToEdit.DeletedOn = user.DeletedOn;
            userToEdit.LockedOn = user.LockedOn;

            userRepository.Edit(userToEdit);
            userRepository.Save();
        }
    }
}