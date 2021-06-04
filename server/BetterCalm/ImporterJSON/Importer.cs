using ImporterInterfaces;
using ImporterModel;
using System;
using System.Collections.Generic;

namespace ImporterJSON
{
	public class Importer : IImporter
	{
		public string GetId()
		{
			throw new NotImplementedException();
		}

		IEnumerable<ContentImport> IImporter.Import(string filePath)
		{
			throw new NotImplementedException();
		}
	}
}
