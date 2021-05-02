using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Repositories
{
	public class UserRepository : IUserRepository
	{
		private DbContext context;
		private DbSet<User> users;

		public UserRepository(DbContext context)
		{
			this.context = context;
			this.users = context.Set<User>();
		}

		public void Add(User user)
		{
			throw new System.NotImplementedException();
		}

		public User Get(string eMail)
		{
			User user = this.users.
				FirstOrDefault(user => user.EMail == eMail);
			if (user == null)
				throw new NotFoundException(eMail);
			return user;
		}
	}
}
