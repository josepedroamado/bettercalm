using Domain;

namespace BL.Utils
{
	public class UserCredentialsValidator
	{
		public static bool ValidateCredentials(User user, string password)
		{
			return user.Password.Equals(password);
		}
	}
}
