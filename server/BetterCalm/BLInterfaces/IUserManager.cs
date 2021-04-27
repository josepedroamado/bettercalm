namespace BLInterfaces
{
	public interface IUserManager
	{
		string Login(string eMail, string password);
		void Logout(string token);
	}
}
