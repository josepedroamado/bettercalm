using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
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

        public AppointmentDiscount Get(double discount)
        {
            AppointmentDiscount appointmentDiscount = this.discounts.FirstOrDefault(stored => stored.Discount == discount);
            if (appointmentDiscount == null)
            {
                throw new NotFoundException("Appointment Discount");
            }
            return appointmentDiscount;
        }

        public IEnumerable<AppointmentDiscount> GetAll()
        {
            if (this.discounts.Count() <= 0)
            {
                throw new CollectionEmptyException("Appointment Discounts");
            }
            else
            {
                return this.discounts;
            }
        }
    }
}
