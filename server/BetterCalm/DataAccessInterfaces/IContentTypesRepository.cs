using Domain;

namespace DataAccessInterfaces
{
	public interface IContentTypesRepository
	{
		ContentType Get(string name);
	}
}
