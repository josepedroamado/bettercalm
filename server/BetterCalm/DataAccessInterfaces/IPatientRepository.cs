using Domain;

namespace DataAccessInterfaces
{
	public interface IPatientRepository
	{
		Patient Get(string eMail);
	}
}
