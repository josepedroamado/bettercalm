using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            PsychologistRate storedRate = this.rates.FirstOrDefault(stored => stored.HourlyRate == rate);
            if (storedRate == null)
                throw new NotFoundException("Psychologist Rate");
            return storedRate;
        }

        public IEnumerable<PsychologistRate> GetAll()
        {
            if (this.rates.Count() <= 0)
                throw new CollectionEmptyException("Psychologist Rates");
            else
                return this.rates;
        }
    }
}
