using BL;
using BLInterfaces;
using DataAccess.Context;
using DataAccess.Repositories;
using DataAccessInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Injector
{
	public class ServicesInjector
	{
		private readonly IServiceCollection services;

		public ServicesInjector(IServiceCollection services)
		{
			this.services = services;
		}

		public void AddBLServices() 
		{
			services.AddScoped<IContentPlayer, ContentPlayer>();
			services.AddScoped<ISessionLogic, SessionLogic>();
			services.AddScoped<IPlaylistLogic, PlaylistLogic>();
		}

		public void AddDataAccessServices()
		{
			services.AddScoped<IPlaylistRepository, PlaylistRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IAdministratorRepository, AdministratorRepository>();
			services.AddScoped<ISessionRepository, SessionRepository>();
			services.AddScoped<IContentRepository, ContentRepository>();
		}

		public void AddContextServices()
		{
			services.AddScoped<DbContext, BetterCalmContext>();
		}
	}
}
