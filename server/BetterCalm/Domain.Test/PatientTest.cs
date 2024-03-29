﻿using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

		[TestMethod]
		public void UpdateFromPatient_DataIsCorrect_Updated()
		{
			Patient original = new Patient()
			{
				Id = 1,
				BirthDate = new DateTime(1990, 1, 1),
				Email = "john.doe@gmail.com",
				FirstName = "John",
				LastName = "Doe",
				Phone = "14785559632"
			};

			AppointmentDiscount discount = new AppointmentDiscount()
			{
				Id = 1,
				Discount = 50.00
			};

			Patient updated = new Patient()
			{
				Id = 1,
				BirthDate = new DateTime(1950, 1, 1),
				Email = "arthur.morgan@gmail.com",
				FirstName = "Arthur",
				LastName = "Morgan",
				Phone = "8885551234",
				AppointmentDiscount = discount
			};

			original.UpdateFromPatient(updated);

			Assert.IsTrue(Equals(original.Id, updated.Id)
				&& !Equals(original.Email, updated.Email)
				&& Equals(original.FirstName, updated.FirstName)
				&& Equals(original.LastName, updated.LastName)
				&& Equals(original.Phone, updated.Phone)
				&& Equals(original.BirthDate, updated.BirthDate));
		}
	}
}
