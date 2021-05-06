using Domain.Exceptions;
using System;

namespace Domain
{
	public class Patient : Person
	{
		public DateTime BirthDate { get; set; }
		public string EMail { get; set; }
		public string Phone { get; set; }

		public override bool Validate()
		{
			if (BirthDate == DateTime.MinValue)
				throw new InvalidInputException("BirthDate is required");
			if (string.IsNullOrEmpty(this.EMail))
				throw new InvalidInputException("EMail is required");
			if (string.IsNullOrEmpty(this.Phone))
				throw new InvalidInputException("Phone is required");
			return base.Validate();
		}
	}
}
