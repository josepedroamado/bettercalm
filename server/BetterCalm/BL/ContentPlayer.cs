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

		public ContentPlayer(IPlaylistRepository playlistRepository, ICategoryRepository categoryRepository)
		{
			this.playlistRepository = playlistRepository;
		}

		public IEnumerable<Playlist> GetPlaylists()
		{
			return this.playlistRepository.Get();
		}

        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
