using Domain;

namespace DataAccessInterfaces
{
	public interface ISessionRepository
	{
		Session Get(string eMail);
		void Add(Session session);
		void Delete(Session session);
	}
}
