using Domain;

namespace DataAccessInterfaces
{
	public interface IAdministratorRepository
	{
		Administrator Get(string eMail);
	}
}
