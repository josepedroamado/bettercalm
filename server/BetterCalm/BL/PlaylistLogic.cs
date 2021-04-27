using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using System.Collections.Generic;

namespace BL
{
	public class PlaylistLogic : IPlaylistLogic
	{
		private readonly IPlaylistRepository playlistRepository;
		public PlaylistLogic(IPlaylistRepository playlistRepository)
		{
			this.playlistRepository = playlistRepository;
		}

		public IEnumerable<Playlist> GetPlaylists()
		{
			return this.playlistRepository.GetAll();
		}

		public Playlist GetPlaylist(int id)
		{
			return this.playlistRepository.Get(id);
		}
	}
}
