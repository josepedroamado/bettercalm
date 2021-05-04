namespace BLInterfaces
{
	public interface ISessionLogic
	{
		string Login(string eMail, string password);
		void Logout(string token);
		bool IsTokenValid(string token);
	}
}
