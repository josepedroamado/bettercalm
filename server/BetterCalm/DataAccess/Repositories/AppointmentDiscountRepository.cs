using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public AppointmentDiscount Get(double? discount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppointmentDiscount> GetAll()
        {
            if (this.discounts.Count() <= 0)
                throw new CollectionEmptyException("Appointment Discounts");
            else
                return this.discounts;
        }
    }
}
