using System;

namespace Domain.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string objectName) 
			: base($"{objectName} was not found") {}
	}
}
