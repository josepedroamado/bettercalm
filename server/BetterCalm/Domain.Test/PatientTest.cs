using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
	[TestClass]
	public class PatientTest
	{
		[TestMethod]
		public void Validate_DataIsCorrect_Validated()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				FirstName = "first name",
				LastName = "last name",
				Email = "email",
				BirthDate = new DateTime(1990, 1, 1),
				Phone = "099099099"
			};

			Assert.IsTrue(patient.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Validate_NoFirstName_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				LastName = "last name",
				Email = "email",
				BirthDate = new DateTime(1990, 1, 1),
				Phone = "099099099"
			};

			Assert.IsFalse(patient.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Validate_NoLastName_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				FirstName = "first name",
				Email = "email",
				BirthDate = new DateTime(1990, 1, 1),
				Phone = "099099099"
			};

			Assert.IsFalse(patient.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Validate_NoEMail_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				FirstName = "first name",
				LastName = "last name",
				BirthDate = new DateTime(1990, 1, 1),
				Phone = "099099099"
			};

			Assert.IsFalse(patient.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Validate_NoBirthDate_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				FirstName = "first name",
				LastName = "last name",
				Email = "email",
				Phone = "099099099"
			};

			Assert.IsFalse(patient.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Validate_NoPhone_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				Id = 1,
				FirstName = "first name",
				LastName = "last name",
				Email = "email",
				BirthDate = new DateTime(1990, 1, 1),
			};

			Assert.IsFalse(patient.Validate());
		}
	}
}
