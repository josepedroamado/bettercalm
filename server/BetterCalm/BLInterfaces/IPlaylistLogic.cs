using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IPlaylistLogic
	{
		public IEnumerable<Playlist> GetPlaylists();

		public Playlist GetPlaylist(int id);
	}
}
