using ImporterModel;

namespace ImporterInterfaces
{
	public interface IImporter
	{
		string GetId();
		ContentImport Import(string filePath);

	}
}
