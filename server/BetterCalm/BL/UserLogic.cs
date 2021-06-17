using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
	public class UserLogic : IUserLogic
	{
		private IUserRepository userRepository;
		private IRoleRepository roleRepository;

		public UserLogic(IUserRepository userRepository, IRoleRepository roleRepository)
		{
			this.userRepository = userRepository;
			this.roleRepository = roleRepository;
		}

		public User GetUser(string email)
		{
			return this.userRepository.Get(email);
		}

		public void CreateUser(User user)
		{
			user.Roles = GetStoredRoles(user.Roles);
			this.userRepository.Add(user);
		}

		private ICollection<Role> GetStoredRoles(ICollection<Role> roles)
		{
			if (roles == null)
            {
				return roles;
			}
			return roles.Select(role => this.roleRepository.Get(role.Name)).ToList();
		}

		public void UpdateUser(User user)
		{
			User currentUser = this.userRepository.Get(user.Id);
			if (currentUser == null)
            {
				return;
			}
			currentUser.UpdateFromUser(user);
			this.userRepository.Update(currentUser);
		}

		public void DeleteUser(int id)
		{
			this.userRepository.Delete(id);
		}

		public ICollection<User> GetUsersByRole(string roleName)
		{
			return this.roleRepository.GetUsers(roleName);
		}
	}
}
