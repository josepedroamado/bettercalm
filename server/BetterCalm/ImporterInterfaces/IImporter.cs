using ImporterModel;
using System.Collections.Generic;

namespace ImporterInterfaces
{
	public interface IImporter
	{
		string GetId();
		IEnumerable<ContentImport> Import(string filePath);

	}
}
