using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
    public interface IPatientLogic
    {
        IEnumerable<Patient> GetAllWithoutDiscountAndRequiredAppointmentQuantity();
    }
}
