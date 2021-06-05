using Domain.Exceptions;

namespace Domain
{
	public abstract class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public virtual bool Validate()
		{
			if (string.IsNullOrWhiteSpace(this.FirstName))
			{
				throw new InvalidInputException("FirstName is required");
			}
			if (string.IsNullOrWhiteSpace(this.LastName))
			{
				throw new InvalidInputException("LastName is required");
			}
			return true;
		}
	}
}
