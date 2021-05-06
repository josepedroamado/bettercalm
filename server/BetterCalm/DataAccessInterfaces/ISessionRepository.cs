using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface ISessionRepository
	{
		Session GetByEmail(string eMail);
		Session GetByToken(string token);
		void Add(Session session);
		void Delete(Session session);
		IEnumerable<Role> GetRoles(string token);
	}
}
