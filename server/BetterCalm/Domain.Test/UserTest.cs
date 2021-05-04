using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
	[TestClass]
	public class UserTest
	{
		[TestMethod]
		public void TestValidateOk()
		{
			User user = new User()
			{
				EMail = "a@a.com",
				Name = "name",
				Password = "password"
			};

			Assert.IsTrue(user.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void TestValidateWithoutEMail()
		{
			User user = new User()
			{
				Name = "name",
				Password = "password"
			};

			Assert.IsFalse(user.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void TestValidateWithoutName()
		{
			User user = new User()
			{
				EMail = "a@a.com",
				Password = "password"
			};

			Assert.IsFalse(user.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void TestValidateWithoutPassword()
		{
			User user = new User()
			{
				EMail = "a@a.com",
				Name = "name"
			};

			Assert.IsFalse(user.Validate());
		}
	}
}
