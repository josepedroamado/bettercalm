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
		private readonly ICategoryRepository categoryRepository;

		public ContentPlayer(IPlaylistRepository playlistRepository, ICategoryRepository categoryRepository)
		{
			this.playlistRepository = playlistRepository;
			this.categoryRepository = categoryRepository;
		}

		public IEnumerable<Playlist> GetPlaylists()
		{
			return this.playlistRepository.GetAll();
		}

		public Playlist GetPlaylist(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Category> GetCategories()
        {
			return this.categoryRepository.GetAll();
		}

        public Category GetCategory(int Id)
        {
			return this.categoryRepository.Get(Id);
        }
	}
}
