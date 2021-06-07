using Domain;
using System;

namespace Model
{
    public class PatientModel
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public double AppointmentDiscount { get; set; }
		public int AppointmentQuantity { get; set; }

		public Patient ToEntity()
		{
			return new Patient
			{
				Id = this.Id,
				FirstName = this.FirstName,
				LastName = this.LastName,
				BirthDate = this.BirthDate,
				Email = this.Email,
				Phone = this.Phone,
				AppointmentDiscount = new AppointmentDiscount()
				{
					Id = 1,
					Discount = this.AppointmentDiscount
				},
				AppointmentQuantity = this.AppointmentQuantity
			};
		}

		public override bool Equals(object obj)
		{
			return obj is PatientModel model &&
				   Id == model.Id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id);
		}

		public PatientModel(Patient patient)
		{
			Id = patient.Id;
			FirstName = patient.FirstName;
			LastName = patient.LastName;
			BirthDate = patient.BirthDate;
			Email = patient.Email;
			Phone = patient.Phone;
			AppointmentDiscount = patient.AppointmentDiscount.Discount;
			AppointmentQuantity = patient.AppointmentQuantity;
		}

		public PatientModel()
		{

		}
	}
}
