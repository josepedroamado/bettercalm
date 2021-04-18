using System;

namespace Domain
{
	public class Patient : Person
	{
		public DateTime BirthDate { get; set; }
		public string EMail { get; set; }
		public string Phone { get; set; }
	}
}
