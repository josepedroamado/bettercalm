using ImporterInterfaces;
using ImporterModel;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ImporterXML
{
	public class Importer : IImporter
	{
		private const string ID = "XML";

		public string GetId()
		{
			return ID;
		}

		public IEnumerable<Content> Import(string filePath)
		{
			if (File.Exists(filePath))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Contents));

				Contents contents;

				using (XmlReader reader = XmlReader.Create(filePath))
				{
					contents = serializer.Deserialize(reader) as Contents;
				}

				return contents;
			}
			else
			{
				throw new FileNotFoundException(filePath);
			}
		}
	}
}
