using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IPlaylistLogic
	{
		public Playlist GetPlaylist(int id);
		public IEnumerable<Playlist> GetPlaylists();
		public IEnumerable<Playlist> GetPlaylists(Category category);
	}
}
