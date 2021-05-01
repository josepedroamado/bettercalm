using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Repositories
{
	public class PatientRepository : IPatientRepository
	{
		private DbContext context;
		private DbSet<Patient> patients;

		public PatientRepository(DbContext context)
		{
			this.context = context;
			this.patients = context.Set<Patient>();
		}

		public Patient Get(string eMail)
		{
			Patient patient = this.patients.FirstOrDefault(patient => patient.EMail.Equals(eMail));
			if (patient == null)
				throw new NotFoundException(eMail);
			return patient;
		}
	}
}
