using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public Illness Get(int id)
        {
            Illness illness = this.illnesses.FirstOrDefault(illness => illness.Id == id);
            if (illness == null)
                throw new NotFoundException(id.ToString());
            return illness;
        }

        public IEnumerable<Illness> GetAll()
        {
            return this.illnesses;
        }
    }
}
