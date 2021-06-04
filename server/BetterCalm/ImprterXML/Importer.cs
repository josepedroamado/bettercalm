using ImporterInterfaces;
using ImporterModel;
using System;
using System.Collections.Generic;

namespace ImporterXML
{
	public class Importer : IImporter
	{
		public string GetId()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ContentImport> Import(string filePath)
		{
			throw new NotImplementedException();
		}
	}
}
