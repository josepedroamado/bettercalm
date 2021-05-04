using Domain;

namespace DataAccessInterfaces
{
	public interface IUserRepository
	{
		User Get(string eMail);
		User Get(int id);
		void Add(User user);
		void Update(User user);
		void Delete(int id);
	}
}
