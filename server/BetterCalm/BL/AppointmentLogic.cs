using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
	public class AppointmentLogic : IAppointmentLogic
	{
		private const int LimitOfAppointmentsPerDay = 5;
		private const string BetterCalmUrl = "http://bettercalm.com.uy/meeting_id/";

		private readonly IPsychologistRepository psychologistRepository;
		private readonly IIllnessRepository illnessRepository;
		private readonly IPatientRepository patientRepository;

		public AppointmentLogic(IPsychologistRepository psychologistRepository, IIllnessRepository illnessRepository, IPatientRepository patientRepository)
		{
			this.psychologistRepository = psychologistRepository;
			this.illnessRepository = illnessRepository;
			this.patientRepository = patientRepository;
		}

		public Appointment CreateAppointment(Patient patient, Illness illness)
		{
			Illness obtainedIllness = this.illnessRepository.Get(illness.Id);
			Patient obtainedPatient = GetPatient(patient);
			Psychologist candidate = GetCandidate(obtainedIllness);

			Appointment appointment = new Appointment()
			{
				Address = CalculateAddress(candidate),
				Date = CalculateAppointmentDate(candidate),
				Illness = obtainedIllness,
				Patient = obtainedPatient,
				Psychologist = candidate
			};

			Schedule scheduleDay = candidate.GetLast();
			
			if (ShouldAddAppointment(scheduleDay, appointment))
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

			this.psychologistRepository.Update(candidate);
			return appointment;

		}

		private Patient GetPatient(Patient patient)
		{
			Patient obtainedPatient;
			try
			{
				obtainedPatient = this.patientRepository.Get(patient.EMail);
			}
			catch (NotFoundException)
			{
				obtainedPatient = patient;
			}
			return obtainedPatient;
		}

		private Psychologist GetCandidate(Illness illness)
		{
			Psychologist candidate = null;
			DateTime until = DateTime.Now;
			while (candidate == null)
			{
				until = GetUntilDate(until);
				candidate = this.psychologistRepository.Get(illness, until, LimitOfAppointmentsPerDay);
			}
			return candidate;
		}

		private DateTime GetUntilDate(DateTime since)
		{
			int daysUntilFriday = (DayOfWeek.Friday - since.DayOfWeek + 7) % 7;
			since = since.AddDays(daysUntilFriday);
			return since.Date;
		}

		private string CalculateAddress(Psychologist candidate)
		{
			if (candidate.Format == Format.OnSite)
				return candidate.Address;
			return string.Concat(BetterCalmUrl, Guid.NewGuid().ToString());
		}

		private DateTime CalculateAppointmentDate(Psychologist candidate)
		{
			DateTime date = DateTime.Now;

			Schedule last = candidate.GetLast();
			if (last != null)
			{
				if (last.GetScheduleDate() <= DateTime.Now.Date)
				{
					return SetNextWorkDay(date);
				}
				if (last.Appointments.Count() < LimitOfAppointmentsPerDay)
					return last.GetScheduleDate();
				return SetNextWorkDay(last.GetScheduleDate().AddDays(1));
			}

			return SetNextWorkDay(date.AddDays(1));
		}

		private DateTime SetNextWorkDay(DateTime date)
		{
			if (date.DayOfWeek == DayOfWeek.Friday)
				date = date.AddDays(3);
			else if (date.DayOfWeek == DayOfWeek.Saturday)
				date = date.AddDays(2);
			else if (date.DayOfWeek == DayOfWeek.Sunday)
				date = date.AddDays(1);
			return date.Date;
		}

		private bool ShouldAddAppointment(Schedule scheduleDay, Appointment appointment)
		{
			return scheduleDay != null &&
				scheduleDay.GetScheduleDate() == appointment.Date.Date &&
				scheduleDay.GetAppointmentsCount() < LimitOfAppointmentsPerDay;
		}
	}
}
