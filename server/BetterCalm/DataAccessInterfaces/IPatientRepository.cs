using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IPatientRepository
	{
		Patient Get(string email);
		IEnumerable<Patient> GetAll();
		void Add(Patient patient);
		void Update(Patient patient);
	}
}
