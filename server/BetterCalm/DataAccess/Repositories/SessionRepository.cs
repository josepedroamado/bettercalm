using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            if (this.sessions.Find(session.Id) != null)
			{
				this.sessions.Remove(session);
				this.context.SaveChanges();
			}
		}

        public Session GetByEmail(string eMail)
		{
			Session session = this.sessions.
				FirstOrDefault(itSession => itSession.User.EMail == eMail);
			return session;
		}

        public Session GetByToken(string token)
        {
			Session session = this.sessions.
				FirstOrDefault(itSession => itSession.Token == token);
			return session;
		}

		public IEnumerable<Role> GetRoles(string token)
		{
			throw new System.NotImplementedException();
		}
	}
}
