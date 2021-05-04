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
			throw new NotImplementedException();
		}
	}
}
