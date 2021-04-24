using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using System;
using System.Collections.Generic;

namespace BL
{
	public class ContentPlayer : IContentPlayer
	{
		private readonly IPlaylistRepository playlistRepository;

		public ContentPlayer(IPlaylistRepository playlistRepository)
		{
			this.playlistRepository = playlistRepository;
		}

		public IEnumerable<Playlist> GetPlaylists()
		{
			return this.playlistRepository.Get();
		}
	}
}
