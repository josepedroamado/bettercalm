using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
			try
			{
				if (Get(user.EMail) != null)
					throw new AlreadyExistsException(user.EMail);
			}
			catch (NotFoundException)
			{
				this.users.Add(user);
				this.context.SaveChanges();
			}
		}

		public User Get(string eMail)
		{
			User user = this.users.
				FirstOrDefault(user => user.EMail == eMail);
			if (user == null)
				throw new NotFoundException(eMail);
			return user;
		}

		private User Get(int id)
		{
			User user = this.users.
				AsNoTracking().
				FirstOrDefault(user => user.Id == id);
			if (user == null)
				throw new NotFoundException(id.ToString());
			return user;
		}

		public void Update(User user)
		{
			User storedUser = Get(user.Id);
			if (storedUser != null)
			{
				this.users.Update(user);
				this.context.SaveChanges();
			}
		}

		public void Delete(int id)
		{
			User user = this.users.FirstOrDefault(user => user.Id == id);
			
			if (user != null)
			{
				this.users.Remove(user);
				this.context.SaveChanges();
			}
		}

		public IEnumerable<User> GetAll()
		{
			throw new System.NotImplementedException();
		}
	}
}
