using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private DbContext context;
		private DbSet<Role> roles;

		public RoleRepository(DbContext context)
		{
			this.context = context;
			this.roles = context.Set<Role>();
		}

		public Role Get(string name)
		{
			Role role = this.roles.FirstOrDefault(role => role.Name.Equals(name));
			if (role == null)
			{
				throw new NotFoundException(name);
			}
			return role;
		}

		public ICollection<User> GetUsers(string name)
		{
			Role role = this.roles.Include(u => u.Users).FirstOrDefault(role => role.Name.Equals(name));
			if (role != null)
			{
				return role.Users;
			}
			return new List<User>();
		}
	}
}
