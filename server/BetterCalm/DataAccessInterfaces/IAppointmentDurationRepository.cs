using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IAppointmentDurationRepository
    {
        IEnumerable<AppointmentDuration> GetAll();
    }
}
