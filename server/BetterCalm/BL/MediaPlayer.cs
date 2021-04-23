using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using System;
using System.Collections.Generic;

namespace BL
{
	public class MediaPlayer : IMediaPlayer
	{
		private readonly IPlaylistRepository playlistRepository;

		public MediaPlayer(IPlaylistRepository playlistRepository)
		{
			this.playlistRepository = playlistRepository;
		}

		public IEnumerable<Playlist> GetPlaylists()
		{
			return this.playlistRepository.Get();
		}
	}
}
