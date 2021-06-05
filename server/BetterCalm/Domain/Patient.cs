using Domain.Exceptions;
using System;

namespace Domain
{
	public class Patient : Person
	{
		public DateTime BirthDate { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public AppointmentDiscount AppointmentDiscount { get; set; }

		public override bool Validate()
		{
			if (BirthDate == DateTime.MinValue)
				throw new InvalidInputException("BirthDate is required");
			if (string.IsNullOrWhiteSpace(this.Email))
				throw new InvalidInputException("Email is required");
			if (string.IsNullOrWhiteSpace(this.Phone))
				throw new InvalidInputException("Phone is required");
			return base.Validate();
		}
	}
}
