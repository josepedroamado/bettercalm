using System.Collections.Generic;

namespace Domain
{
	public class Illness
	{
		public int Id { get; set; } 
		public string Name { get; set; }
		public IEnumerable<Psychologist> Psychologists { get; set; }
	}
}
