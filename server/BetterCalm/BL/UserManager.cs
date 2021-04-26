using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
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
			try
			{
				Administrator administrator = administratorRepository.Get(eMail);
				if (administrator != null && UserCredentialsValidator.ValidateCredentials(administrator, password))
				{
					Session session = this.sessionRepository.Get(eMail);
					if (session == null)
					{
						session = new Session()
						{
							Token = Guid.NewGuid().ToString(),
							User = administrator
						};
						this.sessionRepository.Add(session);
					}
					return session.Token;
				}
				else
					throw new InvalidCredentialsException();
			}
			catch (NotFoundException)
			{
				throw new InvalidCredentialsException();
			}
		}
	}
}
