using System;
using System.Collections.Generic;

namespace Domain
{
	public class Psychologist : Person
	{
		public string Address { get; set; }
		public Format Format { get; set; }
		public IEnumerable<Illness> Illnesses { get; set; }
		public DateTime CreatedDate { get; set; }
		public IEnumerable<Schedule> ScheduleDays { get; set; }
	}
}
