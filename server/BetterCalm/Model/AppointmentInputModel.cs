using Domain;
using System;

namespace Model
{
	public class AppointmentInputModel
	{
		public int IllnessId { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; } 
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Duration { get; set; }

		public Appointment ToEntity()
		{
			return new Appointment()
			{
				Illness = new Illness()
				{
					Id = this.IllnessId
				},
				Patient = new Patient()
				{
					BirthDate = this.BirthDate,
					Email = this.Email,
					FirstName = this.Name,
					LastName = this.LastName,
					Phone = this.Phone
				}
			};
		}
	}
}