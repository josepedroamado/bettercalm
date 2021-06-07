using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using System.Collections.Generic;

namespace BL
{
    public class PatientLogic : IPatientLogic
    {
        private readonly IPatientRepository patientRepository;
        private const int RequiredAppointmentQuantity = 5;

        public PatientLogic(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public IEnumerable<Patient> GetAllWithoutDiscountAndRequiredAppointmentQuantity()
        {
            return this.patientRepository.GetAllWithoutDiscount(RequiredAppointmentQuantity);
        }
    }
}
