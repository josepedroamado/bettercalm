using System;

namespace Domain
{
	public class Appointment
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public Patient Patient { get; set; }
		public Psychologist Psychologist { get; set; }
		public Illness Illness { get; set; }
		public string Address { get; set; }
		public AppointmentDuration Duration { get; set; }
		public AppointmentDiscount Discount { get; set; }
		public double TotalCost { get; set; }
		public DateTime GetDate()
		{
			return Date.Date;
		}
	}
}
