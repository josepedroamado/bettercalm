using Domain;
using System.Collections.Generic;

namespace Model
{
	public class AdministratorInputModel
	{
		private const string DefaultRole = "Administrator";
		public int Id { get; set; }
		public string Name { get; set; }
		public string EMail { get; set; }
		public string Password { get; set; }

		public User ToEntity()
		{
			return new User()
			{
				Id = this.Id,
				EMail = this.EMail,
				Name = this.Name,
				Password = this.Password,
				Roles = new List<Role>()
				{
					new Role()
					{
						Name = DefaultRole
					}
				}
			};
		}
	}
}
