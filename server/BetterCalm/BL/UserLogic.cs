using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
	public class UserLogic : IUserLogic
	{
		private readonly IUserRepository userRepository;
		private readonly IRoleRepository roleRepository;

		public UserLogic(IUserRepository userRepository, IRoleRepository roleRepository)
		{
			this.userRepository = userRepository;
			this.roleRepository = roleRepository;
		}

		public User GetUser(string eMail)
		{
			return this.userRepository.Get(eMail);
		}

		public void CreateUser(User user)
		{
			user.Roles = GetStoredRoles(user.Roles);
			this.userRepository.Add(user);
		}

		private ICollection<Role> GetStoredRoles(ICollection<Role> roles)
		{
			if (roles == null)
				return roles;

			return roles.Select(role => this.roleRepository.Get(role.Name)).ToList();
		}
	}
}
