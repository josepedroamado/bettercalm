using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface ISessionRepository
	{
		Session GetByEmail(string email);
		Session GetByToken(string token);
		void Add(Session session);
		void Delete(Session session);
		IEnumerable<Role> GetRoles(string token);
	}
}
