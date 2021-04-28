using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class IllnessRepository : IIllnessRepository
    {
        private DbContext context;
        private DbSet<Illness> illnesses;

        public IllnessRepository(DbContext context)
        {
            this.context = context;
            this.illnesses = context.Set<Illness>();
        }
        public IEnumerable<Illness> GetAll()
        {
            return this.illnesses;
        }
    }
}
