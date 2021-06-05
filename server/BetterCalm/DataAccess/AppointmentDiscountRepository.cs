using DataAccessInterfaces;
using Domain;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class AppointmentDiscountRepository : IAppointmentDiscountRepository
    {
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
