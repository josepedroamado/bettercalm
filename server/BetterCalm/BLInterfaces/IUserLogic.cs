using Domain;

namespace BLInterfaces
{
	public interface IUserLogic
	{
		void CreateUser(User user);
		User GetUser(string eMail);
	}
}
