using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;

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
			throw new NotImplementedException();
		}
	}
}
