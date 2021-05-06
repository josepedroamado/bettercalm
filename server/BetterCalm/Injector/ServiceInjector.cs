﻿using BL;
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
			services.AddScoped<IContentLogic, ContentLogic>();
			services.AddScoped<ISessionLogic, SessionLogic>();
			services.AddScoped<IPlaylistLogic, PlaylistLogic>();
			services.AddScoped<ICategoryLogic, CategoryLogic>();
			services.AddScoped<IIllnessLogic, IllnessLogic>();
			services.AddScoped<IPsychologistLogic, PsychologistLogic>();
			services.AddScoped<IAppointmentLogic, AppointmentLogic>();
			services.AddScoped<IUserLogic, UserLogic>();
		}

		public void AddDataAccessServices()
		{
			services.AddScoped<IPlaylistRepository, PlaylistRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ISessionRepository, SessionRepository>();
			services.AddScoped<IContentRepository, ContentRepository>();
			services.AddScoped<IIllnessRepository, IllnessRepository>();
			services.AddScoped<IPsychologistRepository, PsychologistRepository>();
			services.AddScoped<IPatientRepository, PatientRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
		}

		public void AddContextServices()
		{
			services.AddScoped<DbContext, BetterCalmContext>();
		}
	}
}