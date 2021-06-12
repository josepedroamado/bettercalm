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
			if (user.Validate())
			{
				try
				{
					if (Get(user.Email) != null)
						throw new AlreadyExistsException(user.Email);
				}
				catch (NotFoundException)
				{
					this.users.Add(user);
					this.context.SaveChanges();
				}
			}
		}

		public User Get(string email)
		{
			User user = this.users.
				FirstOrDefault(user => user.Email == email);
			if (user == null)
				throw new NotFoundException(email);
			return user;
		}

		public User Get(int id)
		{
			User user = this.users.
				FirstOrDefault(user => user.Id == id);
			if (user == null)
				throw new NotFoundException(id.ToString());
			return user;
		}

		public void Update(User user)
		{
			if (user.Validate() && Get(user.Id) != null)
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
	}
}
