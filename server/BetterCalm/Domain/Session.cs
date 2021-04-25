namespace Domain
{
	public class Session
	{
		public int Id { get; set; }
		public string EMail { get; set; }
		public string Token { get; set; }
		public User User { get; set; }

		public string GetSessionEmail()
		{
			return User.EMail;
		}
	}
}
