using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class AppointmentDiscountRepository : IAppointmentDiscountRepository
    {
        private DbContext context;
        private DbSet<AppointmentDiscount> discounts;

        public AppointmentDiscountRepository(DbContext context)
        {
            this.context = context;
            this.discounts = context.Set<AppointmentDiscount>();
        }

        public AppointmentDiscount Get(int? discount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppointmentDiscount> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
