using ImporterInterfaces;
using ImporterModel;
using System;
using System.Collections.Generic;

namespace ImporterJSON
{
	public class Importer : IImporter
	{
		private const string ID = "JSON";

		public string GetId()
		{
			return ID;
		}

		IEnumerable<ContentImport> IImporter.Import(string filePath)
		{
			throw new NotImplementedException();
		}
	}
}
