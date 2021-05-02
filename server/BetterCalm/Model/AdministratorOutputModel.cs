using Domain;

namespace Model
{
	public class AdministratorOutputModel
	{
		public int Id { get; set; }
		public string EMail { get; set; }

		public AdministratorOutputModel(User user)
		{
			this.Id = user.Id;
			this.EMail = user.EMail;
		}
	}
}
