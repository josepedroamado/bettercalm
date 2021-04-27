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
		private readonly IContentRepository contentRepository;

		public ContentPlayer(IPlaylistRepository playlistRepository, 
			ICategoryRepository categoryRepository, 
			IContentRepository contentRepository)
		{
			this.playlistRepository = playlistRepository;
			this.categoryRepository = categoryRepository;
			this.contentRepository = contentRepository;
		}

		public IEnumerable<Playlist> GetPlaylists()
		{
			return this.playlistRepository.GetAll();
		}

		public Playlist GetPlaylist(int id)
		{
			return this.playlistRepository.Get(id);
		}

		public IEnumerable<Category> GetCategories()
        {
			return this.categoryRepository.GetAll();
		}

        public Category GetCategory(int Id)
        {
			return this.categoryRepository.Get(Id);
        }

		public IEnumerable<Content> GetContents()
		{
			return this.contentRepository.GetAll();
		}
	}
}
