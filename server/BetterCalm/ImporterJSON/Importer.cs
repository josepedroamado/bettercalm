using ImporterInterfaces;
using ImporterModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
			if (File.Exists(filePath))
			{
				return JsonConvert.DeserializeObject<IEnumerable<ContentImport>>(File.ReadAllText(filePath));
			}
			else
			{
				throw new FileNotFoundException(filePath);
			}
		}
	}
}
