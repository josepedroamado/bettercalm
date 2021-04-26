using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Repositories
{
	public class SessionRepository : ISessionRepository
	{
		private DbContext context;
		private DbSet<Session> sessions;

		public SessionRepository(DbContext context)
		{
			this.context = context;
			this.sessions = context.Set<Session>();
		}

		public void Add(Session session)
		{
			this.sessions.Add(session);
			this.context.SaveChanges();
		}

        public void Delete(Session session)
        {
            if (this.sessions.Find(session.Id) == null)
            {
				throw new NotFoundException($"The session for {session.GetSessionEmail()} ");
            }
            else
            {
				this.sessions.Remove(session);
				this.context.SaveChanges();
            }
		}

        public Session Get(string eMail)
		{
			Session session = this.sessions.
				FirstOrDefault(itSession => itSession.User.EMail == eMail);
			return session;
		}
	}
}
