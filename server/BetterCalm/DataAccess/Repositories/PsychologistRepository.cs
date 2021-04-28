using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Repositories
{
    public class PsychologistRepository : IPsychologistRepository
    {
        private DbContext context;
        private DbSet<Psychologist> psychologists;

        public PsychologistRepository(DbContext context)
        {
            this.context = context;
            this.psychologists = context.Set<Psychologist>();
        }

        public Psychologist Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Psychologist psychologist)
        {
            throw new NotImplementedException();
        }
    }
}
