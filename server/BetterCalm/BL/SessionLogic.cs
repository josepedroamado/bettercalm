using BL.Utils;
using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using System;
using System.Linq;

namespace BL
{
	public class SessionLogic : ISessionLogic
	{
		private ISessionRepository sessionRepository;
		private IUserRepository userRepository;

		public SessionLogic(ISessionRepository sessionRepository, IUserRepository userRepository)
		{
			this.sessionRepository = sessionRepository;
			this.userRepository = userRepository;
		}

		public bool TokenHasRole(string token, string role)
		{
			return this.sessionRepository.GetRoles(token).Any(tokenRole => tokenRole.Name.Equals(role));
		}

		public bool IsTokenValid(string token)
		{
			return this.sessionRepository.GetByToken(token) != null;
		}

		public string Login(string email, string password)
		{
			try
            {
                User user = userRepository.Get(email);
                return GetCurrentOrNewToken(email, password, user);
            }
            catch (NotFoundException)
			{
				throw new InvalidCredentialsException();
			}
		}

        private string GetCurrentOrNewToken(string email, string password, User user)
        {
            string token;
            if (UserCredentialsValidator.ValidateCredentials(user, password))
            {
                Session session = this.sessionRepository.GetByEmail(email);
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
