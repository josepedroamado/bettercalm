using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IImporterLogic
	{
		void Import(string type, string filePath);
		IEnumerable<string> GetTypes();
	}
}
