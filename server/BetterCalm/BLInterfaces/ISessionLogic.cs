namespace BLInterfaces
{
	public interface ISessionLogic
	{
		string Login(string eMail, string password);
		void Logout(string token);
	}
}
