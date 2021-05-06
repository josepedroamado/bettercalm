using System;

namespace Domain.Exceptions
{
	public class AlreadyExistsException : Exception
	{
		public AlreadyExistsException(string objectName)
			: base($"{objectName} already exists.") { }
	}
}