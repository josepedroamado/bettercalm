using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

		[TestMethod]
		public void TestUpdateFromUserOk()
		{
			User toUpdate = new User();
			User user = new User()
			{
				EMail = "a@a.com",
				Name = "name",
				Password = "password",
				Roles = new List<Role>()
				{
					new Role()
					{
						Id = 1,
						Description = "Administrator"
					}
				}
			};

			toUpdate.UpdateFromUser(user);

			Assert.IsTrue(Equals(user.EMail, toUpdate.EMail) &&
				Equals(user.Name, toUpdate.Name) &&
				Equals(user.Password, toUpdate.Password) &&
				user.Roles.SequenceEqual(toUpdate.Roles));
		}
	}
}
