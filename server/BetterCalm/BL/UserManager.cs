using BLInterfaces;
using DataAccessInterfaces;
using System;

namespace BL
{
	public class UserManager : IUserManager
	{
		private readonly ISessionRepository sessionRepository;
		private readonly IAdministratorRepository administratorRepository;

		public UserManager(ISessionRepository sessionRepository, IAdministratorRepository administratorRepository)
		{
			this.sessionRepository = sessionRepository;
			this.administratorRepository = administratorRepository;
		}

		public string Login(string eMail, string password)
		{
			throw new NotImplementedException();
		}
	}
}
