namespace BLInterfaces
{
	public interface ISessionLogic
	{
		string Login(string email, string password);
		void Logout(string token);
		bool IsTokenValid(string token);
		bool TokenHasRole(string token, string role);
	}
}
