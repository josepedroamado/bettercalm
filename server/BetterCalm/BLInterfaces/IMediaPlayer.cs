using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IMediaPlayer
	{
		public IEnumerable<Playlist> GetPlaylists();
	}
}
