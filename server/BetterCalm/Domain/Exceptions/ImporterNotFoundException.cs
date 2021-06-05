using System;

namespace Domain.Exceptions
{
	public class ImporterNotFoundException : Exception
	{
		public ImporterNotFoundException(string type, string path) : base($"Importer ${type} was not found at {path}.") {}
	}
}
