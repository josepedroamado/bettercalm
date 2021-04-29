using System;

namespace Domain.Exceptions
{
	public class MissingCategoriesException : Exception
	{
		public MissingCategoriesException() : 
			base("Missing categories. At least one category is required.") {}
	}
}
