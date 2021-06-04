using ImporterInterfaces;
using ImporterModel;
using System;
using System.Collections.Generic;

namespace ImporterXML
{
	public class Importer : IImporter
	{
		private const string ID = "XML";

		public string GetId()
		{
			return ID;
		}

		public IEnumerable<ContentImport> Import(string filePath)
		{
			throw new NotImplementedException();
		}
	}
}
