using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IRoleRepository
	{
		Role Get(string name);
		ICollection<User> GetUsers(string name);
	}
}
