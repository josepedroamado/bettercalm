using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
	[Table("Users")]
	public abstract class User
	{
		public int Id { get; set; }
		public string EMail { get; set; }
		public string Password { get; set; }
	}
}
