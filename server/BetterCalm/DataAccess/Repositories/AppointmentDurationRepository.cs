using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class AppointmentDurationRepository : IAppointmentDurationRepository
    {
        private DbContext context;
        private DbSet<AppointmentDuration> durations;

        public AppointmentDurationRepository(DbContext context)
        {
            this.context = context;
            this.durations = context.Set<AppointmentDuration>();
        }

       public IEnumerable<AppointmentDuration> GetAll(){
           throw new NotImplementedException();
       }
    }
}
