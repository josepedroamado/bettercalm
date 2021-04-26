using Domain;

namespace BL
{
	public class UserCredentialsValidator
	{
		public static bool ValidateCredentials(User user, string password)
		{
			return user.Password.Equals(password);
		}
	}
}
