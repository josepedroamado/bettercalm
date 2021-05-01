using System;

namespace Model
{
	public class AppointmentInputModel
	{
		public int IllnessId { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; } 
		public string EMail { get; set; }
		public string Phone { get; set; }
	}
}