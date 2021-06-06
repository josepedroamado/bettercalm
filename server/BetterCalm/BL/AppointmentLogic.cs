using BL.Utils;
using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace BL
{
    public class AppointmentLogic : IAppointmentLogic
	{
		private const int LimitOfAppointmentsPerDay = 5;
		private const string BetterCalmUrl = "http://bettercalm.com.uy/meeting_id/";

		private readonly IPsychologistRepository psychologistRepository;
		private readonly IIllnessRepository illnessRepository;
		private readonly IPatientRepository patientRepository;

		private readonly IAppointmentDurationRepository appointmentDurationRepository;

		public AppointmentLogic(IPsychologistRepository psychologistRepository, 
								IIllnessRepository illnessRepository, 
								IPatientRepository patientRepository, 
								IAppointmentDurationRepository appointmentDurationRepository)
		{
			this.psychologistRepository = psychologistRepository;
			this.illnessRepository = illnessRepository;
			this.patientRepository = patientRepository;
			this.appointmentDurationRepository = appointmentDurationRepository;
		}

		public Appointment CreateAppointment(Appointment newAppointment)
        {
            Illness obtainedIllness = this.illnessRepository.Get(newAppointment.Illness.Id);
            Patient obtainedPatient = GetPatient(newAppointment.Patient);
            Psychologist candidate = GetCandidate(obtainedIllness);
            AppointmentDuration appointmentDuration = this.appointmentDurationRepository.Get(newAppointment.Duration.Duration);

            Appointment appointment = new Appointment()
            {
                Address = CalculateAddress(candidate),
                Date = DateCalculator.CalculateAppointmentDate(candidate, LimitOfAppointmentsPerDay),
                Illness = obtainedIllness,
                Patient = obtainedPatient,
                Psychologist = candidate,
                Duration = appointmentDuration,
				Discount = obtainedPatient.AppointmentDiscount
            };

            Schedule scheduleDay = candidate.GetLast();

            if (scheduleDay != null && scheduleDay.GetScheduleDate() == appointment.Date.Date)
                scheduleDay.Appointments.Add(appointment);
            else
                candidate.ScheduleDays.Add(
                    new Schedule()
                    {
                        Appointments = new List<Appointment>()
                        {
                            appointment
                        },
                        Date = appointment.GetDate(),
                        Psychologist = candidate
                    });
			appointment.TotalCost = CostCalculator.CalculateTotalCost(obtainedPatient.AppointmentDiscount, candidate.Rate.HourlyRate, appointmentDuration.Duration.TotalHours);
            if (obtainedPatient.AppointmentDiscount != null)
            {
				obtainedPatient.AppointmentDiscount = null;
            }
			this.psychologistRepository.Update(candidate);
            return appointment;
        }

        private Patient GetPatient(Patient patient)
		{
			try
			{
				patient = this.patientRepository.Get(patient.Email);
			}
			catch (NotFoundException) 
			{
				this.patientRepository.Add(patient);
			}
			return patient;
		}

		private Psychologist GetCandidate(Illness illness)
		{
			Psychologist candidate = null;
			DateTime until = DateTime.Now;
			while (candidate == null)
			{
				until = DateCalculator.CalculateUntilDate(until);
				candidate = this.psychologistRepository.Get(illness, until, LimitOfAppointmentsPerDay);
			}
			return candidate;
		}

		private string CalculateAddress(Psychologist candidate)
		{
			if (candidate.Format == Format.OnSite)
				return candidate.Address;
			return string.Concat(BetterCalmUrl, Guid.NewGuid().ToString());
		}
    }
}
