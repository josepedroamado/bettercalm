using System;
using System.Collections.Generic;

namespace Domain
{
	public class Schedule
	{
		private ICollection<Appointment> appointments = new List<Appointment>();
		public int Id { get; set; }
		public Psychologist Psychologist { get; set; }
		public DateTime Date { get; set; }
		public ICollection<Appointment> Appointments
		{
			get => appointments;
			set => appointments = value;
		}

		public int GetAppointmentsCount()
		{
			return Appointments.Count;
		}

		public DateTime GetScheduleDate()
		{
			return Date.Date;
		}
	}
}
