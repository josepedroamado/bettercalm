using Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Domain
{
	public class User
	{
		public int Id { get; set; }
		public string EMail { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public ICollection<Role> Roles {get; set; }

		public bool Validate()
		{
			if (string.IsNullOrEmpty(EMail))
				throw new InvalidInputException("EMail is required");
			if (string.IsNullOrEmpty(Password))
				throw new InvalidInputException("Password is required");
			if (string.IsNullOrEmpty(Name))
				throw new InvalidInputException("Name is required");

			return true;
		}

		public void UpdateFromUser(User user)
		{
			this.Id = user.Id;

			if (!string.IsNullOrEmpty(user.Name))
				this.Name = user.Name;			
			if (!string.IsNullOrEmpty(user.EMail))
				this.EMail = user.EMail;			
			if (!string.IsNullOrEmpty(user.Password))
				this.Password = user.Password;
			if (user.Roles != null)
				this.Roles = user.Roles;
		}
	}
}
