using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using System;

namespace BL
{
	public class SessionLogic : ISessionLogic
	{
		private readonly ISessionRepository sessionRepository;
		private readonly IUserRepository userRepository;

		public SessionLogic(ISessionRepository sessionRepository, IUserRepository userRepository)
		{
			this.sessionRepository = sessionRepository;
			this.userRepository = userRepository;
		}

		public bool IsValidToken(string token)
		{
			return this.sessionRepository.GetByToken(token) != null;
		}

		public string Login(string eMail, string password)
		{
			string token = string.Empty;
			try
			{
				User user = userRepository.Get(eMail);
                if (UserCredentialsValidator.ValidateCredentials(user, password))
                {
					Session session = this.sessionRepository.GetByEmail(eMail);
					if (session == null)
					{
						session = new Session()
						{
							Token = Guid.NewGuid().ToString(),
							User = user
						};
						this.sessionRepository.Add(session);
					}
					token = session.Token;
				}
				else
					throw new InvalidCredentialsException();
			}
			catch (NotFoundException)
			{
				throw new InvalidCredentialsException();
			}
			return token;
		}

        public void Logout(string token)
        {
			Session sessionToDelete = this.sessionRepository.GetByToken(token);
			if (sessionToDelete != null)
            {
				this.sessionRepository.Delete(sessionToDelete);
            }
        }
    }
}
