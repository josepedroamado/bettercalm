using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IUserRepository
	{
		User Get(string eMail);
		void Add(User user);
		void Update(User user);
		void Delete(int id);
		IEnumerable<User> GetAll();
	}
}
