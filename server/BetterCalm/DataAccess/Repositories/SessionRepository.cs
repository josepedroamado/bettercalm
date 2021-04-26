using DataAccessInterfaces;
using Domain;
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
            throw new System.NotImplementedException();
        }

        public Session Get(string eMail)
		{
			Session session = this.sessions.
				FirstOrDefault(itSession => itSession.User.EMail == eMail);
			return session;
		}
	}
}
