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
		public void Get_PatientFound_Fetched()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				Email = "patient@gmail.com",
				FirstName = "Patient",
				LastName = "Perez",
				Id = 1,
				Phone = "091569874"
			};
			this.context.Add(patient);
			this.context.SaveChanges();

			PatientRepository repository = new PatientRepository(this.context);
			Patient obtained = repository.Get(patient.Email);
			Assert.AreEqual(obtained, patient);
		}
		
		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Get_PatientNotFound_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				Email = "patient@gmail.com",
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

		[TestMethod]
		public void Add_DataIsCorrect_Added()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				BirthDate = new DateTime(1990, 1, 1),
				Email = "a@a.com",
				FirstName = "first name",
				LastName = "last name",
				Phone = "099099099"
			};

			PatientRepository repository = new PatientRepository(this.context);
			repository.Add(patient);
			Patient obtained = repository.Get(patient.Email);

			Assert.AreEqual(patient, obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(AlreadyExistsException))]
		public void Add_AlreadyExists_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				BirthDate = new DateTime(1990, 1, 1),
				Email = "a@a.com",
				FirstName = "first name",
				LastName = "last name",
				Phone = "099099099"
			};

			PatientRepository repository = new PatientRepository(this.context);
			repository.Add(patient);
			repository.Add(patient);
			Patient obtained = repository.Get(patient.Email);

			Assert.AreEqual(patient, obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Add_NoBirthDate_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				Email = "a@a.com",
				FirstName = "first name",
				LastName = "last name",
				Phone = "099099099"
			};

			PatientRepository repository = new PatientRepository(this.context);
			repository.Add(patient);
			Patient obtained = repository.Get(patient.Email);

			Assert.IsNull(obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Add_NoEMail_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				BirthDate = new DateTime(1990, 1, 1),
				FirstName = "first name",
				LastName = "last name",
				Phone = "099099099"
			};

			PatientRepository repository = new PatientRepository(this.context);
			repository.Add(patient);
			Patient obtained = repository.Get(patient.Email);

			Assert.IsNull(obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Add_NoFirstName_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				BirthDate = new DateTime(1990, 1, 1),
				Email = "a@a.com",
				LastName = "last name",
				Phone = "099099099"
			};

			PatientRepository repository = new PatientRepository(this.context);
			repository.Add(patient);
			Patient obtained = repository.Get(patient.Email);

			Assert.IsNull(obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Add_NoLastName_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				BirthDate = new DateTime(1990, 1, 1),
				Email = "a@a.com",
				FirstName = "first name",
				Phone = "099099099"
			};

			PatientRepository repository = new PatientRepository(this.context);
			repository.Add(patient);
			Patient obtained = repository.Get(patient.Email);

			Assert.IsNull(obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Add_NoPhone_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				BirthDate = new DateTime(1990, 1, 1),
				Email = "a@a.com",
				FirstName = "first name",
				LastName = "last name"
			};

			PatientRepository repository = new PatientRepository(this.context);
			repository.Add(patient);
			Patient obtained = repository.Get(patient.Email);

			Assert.IsNull(obtained);
		}
	}
}
