using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataAccess.Repositories
{
	public class AdministratorRepository : IAdministratorRepository
	{
		private DbContext context;
		private DbSet<Administrator> administrators;

		public AdministratorRepository(DbContext context)
		{
			this.context = context;
			this.administrators = context.Set<Administrator>();
		}


		public Administrator Get(string eMail)
		{
			Administrator administrator = this.administrators.
				FirstOrDefault(admin => admin.EMail == eMail);
			if (administrator == null)
				throw new NotFoundException(eMail);
			return administrator;
		}
	}
}
