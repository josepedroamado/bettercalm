using Domain;
using System;

namespace Model
{
	public class AppointmentOutputModel
	{
		public AppointmentOutputModel() { }
		public AppointmentOutputModel(Appointment appointment)
		{
			this.PsychologistName = appointment.Psychologist.GetFullName();
			this.Format = appointment.Psychologist.Format.ToString();
			this.Address = appointment.Address;
			this.Date = appointment.Date;
			this.Cost = appointment.TotalCost;
			this.Discount = appointment.Discount.Discount;
		}

		public string PsychologistName { get; set; }
		public string Format { get; set; }
		public string Address { get; set; }
		public DateTime Date { get; set; }
		public double Cost { get; set; }
		public double Discount { get; set; }

	}
}