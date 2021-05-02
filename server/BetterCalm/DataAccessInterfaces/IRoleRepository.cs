using Domain;

namespace DataAccessInterfaces
{
	public interface IRoleRepository
	{
		Role Get(string name);
	}
}
