using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
    public interface IPatientLogic
    {
        IEnumerable<Patient> GetAllWithoutDiscountAndRequiredAppointmentQuantity();

        Patient Get(string email);

        void Update(Patient patient);
    }
}
