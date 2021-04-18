using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IPlaylistRepository
	{
		Playlist Get(int id);
		IEnumerable<Playlist> GetAll();
		void Add(Playlist playlist);
	}
}
