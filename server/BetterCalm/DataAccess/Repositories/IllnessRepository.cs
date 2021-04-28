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

        public IllnessRepository(DbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Illness> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
