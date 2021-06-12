using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IUserLogic
	{
		void CreateUser(User user);
		void UpdateUser(User user);
		void DeleteUser(int id);
		ICollection<User> GetUsersByRole(string roleName);
		User GetUser(string email);
	}
}
