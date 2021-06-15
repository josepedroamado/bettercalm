using Domain;
using System;

namespace Model
{
	public class AppointmentOutputModel
	{
		public string PsychologistName { get; set; }
		public string Format { get; set; }
		public string Address { get; set; }
		public DateTime Date { get; set; }
		public double Cost { get; set; }
		public double Discount { get; set; }

		public AppointmentOutputModel() { }

		public AppointmentOutputModel(Appointment appointment)
		{
            if (appointment.Discount == null)
            {
				this.Discount = 0;
			}
            else
            {
				this.Discount = appointment.Discount.Discount;
			}
			this.PsychologistName = appointment.Psychologist.GetFullName();
			this.Format = appointment.Psychologist.Format.ToString();
			this.Address = appointment.Address;
			this.Date = appointment.Date;
			this.Cost = appointment.TotalCost;
		}
	}
}