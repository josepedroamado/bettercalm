using ImporterModel;
using System.Collections.Generic;

namespace ImporterInterfaces
{
	public interface IImporter
	{
		string GetId();
		IEnumerable<Content> Import(string filePath);

	}
}
