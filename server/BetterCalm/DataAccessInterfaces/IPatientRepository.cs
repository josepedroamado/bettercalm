using Domain;

namespace DataAccessInterfaces
{
	public interface IPatientRepository
	{
		Patient GetPatient(string eMail);
	}
}
