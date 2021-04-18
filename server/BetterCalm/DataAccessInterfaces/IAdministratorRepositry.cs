using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IAdministratorRepositry
	{
		void Add(Administrator admin);
		Administrator Get(int id);
		IEnumerable<Administrator> GetAll();
		void Update(Administrator admin);
		void Delete(Administrator admin);
	}
}
