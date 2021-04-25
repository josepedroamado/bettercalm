using BLInterfaces;
using DataAccessInterfaces;
using System;

namespace BL
{
	public class UserManager : IUserManager
	{
		private readonly ISessionRepository sessionRepository;

		public UserManager(ISessionRepository sessionRepository)
		{
			this.sessionRepository = sessionRepository;
		}

		public string Login(string eMail, string password)
		{
			throw new NotImplementedException();
		}
	}
}
