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
			return this.sessions.FirstOrDefault(itSession => itSession.User.Email == eMail);
		}

        public Session GetByToken(string token)
        {
			return this.sessions.FirstOrDefault(itSession => itSession.Token == token);
		}

		public IEnumerable<Role> GetRoles(string token)
		{
			return this.sessions.Include(u => u.User).Include(u => u.User.Roles)
				.FirstOrDefault(session => session.Token.Equals(token))?.User?.Roles;
		}
	}
}
