using Domain;

namespace DataAccessInterfaces
{
	public interface ISessionRepository
	{
		Session Get(string eMail);
	}
}
