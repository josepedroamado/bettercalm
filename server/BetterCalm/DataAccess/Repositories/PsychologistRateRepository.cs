using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class PsychologistRateRepository : IPsychologistRateRepository
    {
        private DbContext context;
        private DbSet<PsychologistRate> rates;

        public PsychologistRateRepository(DbContext context)
        {
            this.context = context;
            this.rates = context.Set<PsychologistRate>();
        }

        public PsychologistRate Get(int rate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PsychologistRate> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
