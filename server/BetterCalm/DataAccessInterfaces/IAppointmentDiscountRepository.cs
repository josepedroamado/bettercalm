using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IAppointmentDiscountRepository
    {
        IEnumerable<AppointmentDiscount> GetAll();

        AppointmentDiscount Get(int? discount);
    }
}
