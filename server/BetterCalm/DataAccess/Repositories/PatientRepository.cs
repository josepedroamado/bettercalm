using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;

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
			throw new NotImplementedException();
		}
	}
}
