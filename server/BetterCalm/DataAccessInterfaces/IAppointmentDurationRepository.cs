using Domain;
using System;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IAppointmentDurationRepository
    {
        IEnumerable<AppointmentDuration> GetAll();

        AppointmentDuration Get(TimeSpan duration);
    }
}
