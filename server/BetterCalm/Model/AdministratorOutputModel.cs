using Domain;

namespace Model
{
	public class AdministratorOutputModel
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }

		public AdministratorOutputModel(User user)
		{
			this.Id = user.Id;
			this.Email = user.Email;
			this.Name = user.Name;
		}
	}
}
