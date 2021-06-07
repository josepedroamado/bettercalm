using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

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
            IEnumerable<Patient> patients = this.patientRepository.GetAllWithoutDiscount(RequiredAppointmentQuantity);
            if (patients.Count() == 0)
            {
                throw new NoPatientsMeetCriteriaException();
            }
            return patients;
        }

        public void Update(Patient patient)
        {
            throw new System.NotImplementedException();
        }

        public Patient Get(string email)
        {
            return this.patientRepository.Get(email);
        }
    }
}
