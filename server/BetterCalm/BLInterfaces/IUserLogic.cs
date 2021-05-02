using Domain;

namespace BLInterfaces
{
	public interface IUserLogic
	{
		void CreateUser(User user);
		void UpdateUser(User user);
		void DeleteUser(int id);
	}
}
