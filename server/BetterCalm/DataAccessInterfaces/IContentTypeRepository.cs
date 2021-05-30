using Domain;

namespace DataAccessInterfaces
{
	public interface IContentTypeRepository
	{
		ContentType Get(string name);
	}
}
