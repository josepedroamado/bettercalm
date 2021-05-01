using System;
using System.Collections.Generic;

namespace Domain
{
	public class Schedule
	{
		public int Id { get; set; }
		public Psychologist Psychologist { get; set; }
		public DateTime Date { get; set; }
		public IEnumerable<Appointment> Appointments { get; set; }
	}
}
