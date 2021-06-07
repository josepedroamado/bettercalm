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
        private readonly IAppointmentDiscountRepository discountRepository;
        private const int RequiredAppointmentQuantity = 5;

        public PatientLogic(IPatientRepository patientRepository, IAppointmentDiscountRepository discountRepository)
        {
            this.patientRepository = patientRepository;
            this.discountRepository = discountRepository;
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
            patient.AppointmentDiscount = this.discountRepository.Get(patient.AppointmentDiscount.Discount);
            this.patientRepository.Update(patient);
        }

        public Patient Get(string email)
        {
            return this.patientRepository.Get(email);
        }
    }
}
