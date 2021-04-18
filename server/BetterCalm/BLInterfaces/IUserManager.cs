using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IUserManager
	{
		void CreateAdministrator();
		void UpdateAdministrator(Administrator admin);
		void DeleteAdministrator(int id);
		string CreateSession(Administrator admin);
		IEnumerable<Administrator> GetAdministrators();
	}
}
