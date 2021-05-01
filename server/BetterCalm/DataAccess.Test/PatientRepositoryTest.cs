using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Domain.Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Common;

namespace DataAccess.Test
{
	[TestClass]
	public class PatientRepositoryTest
	{
		private DbContext context;
		private DbConnection connection;

		public PatientRepositoryTest()
		{
			this.connection = new SqliteConnection("Filename=:memory:");
			this.context = new BetterCalmContext(
				new DbContextOptionsBuilder<BetterCalmContext>()
				.UseSqlite(connection)
				.Options);
		}


		[TestInitialize]
		public void Setup()
		{
			this.connection.Open();
			this.context.Database.EnsureCreated();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			this.context.Database.EnsureDeleted();
		}

		[TestMethod]
		public void GetPatientOk()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				EMail = "patient@gmail.com",
				FirstName = "Patient",
				LastName = "Perez",
				Id = 1,
				Phone = "091569874"
			};
			this.context.Add(patient);
			this.context.SaveChanges();

			PatientRepository repository = new PatientRepository(this.context);
			Patient obtained = repository.Get(patient.EMail);
			Assert.AreEqual(obtained, patient);
		}
		
		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void GetPatientNotFound()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				EMail = "patient@gmail.com",
				FirstName = "Patient",
				LastName = "Perez",
				Id = 1,
				Phone = "091569874"
			};
			this.context.Add(patient);
			this.context.SaveChanges();

			PatientRepository repository = new PatientRepository(this.context);
			Patient obtained = repository.Get("notFoundEmail@fail.com");
			Assert.AreNotEqual(obtained, patient);
		}
	}
}
