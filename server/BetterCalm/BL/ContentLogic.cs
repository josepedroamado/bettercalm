using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
	public class ContentLogic : IContentLogic
	{
		private readonly IContentRepository contentRepository;
		private readonly IPlaylistRepository playlistRepository;
		private readonly ICategoryRepository categoryRepository;

		public ContentLogic(IContentRepository contentRepository, 
			IPlaylistRepository playlistRepository, 
			ICategoryRepository categoryRepository)
		{
			this.contentRepository = contentRepository;
			this.playlistRepository = playlistRepository;
			this.categoryRepository = categoryRepository;
		}

		public void CreateContent(Content content)
		{
			if (content.Categories != null)
			{
				List<Category> storedCategories = content.Categories.Select(category =>
				{
					Category stored = this.categoryRepository.Get(category.Id);
					if (stored != null)
						return stored;
					throw new NotFoundException(category.Id.ToString());
				}).ToList();
				content.Categories = storedCategories;
			}

			if (content.PlayLists != null)
			{
				List<Playlist> storedPlaylists = content.PlayLists.Select(playlist =>
				{
					Playlist stored = this.playlistRepository.Get(playlist.Id);

					if (stored != null)
						return stored;
					if (!string.IsNullOrEmpty(playlist.Name))
						return playlist;

					throw new UnableToCreatePlaylistException();
				}).ToList();
				content.PlayLists = storedPlaylists;
			}
			
			this.contentRepository.Add(content);
		}

		public Content GetContent(int id)
		{
			return this.contentRepository.Get(id);
		}

		public IEnumerable<Content> GetContents()
		{
			return this.contentRepository.GetAll();
		}

        public IEnumerable<Content> GetContents(Playlist playlist)
        {
			return this.contentRepository.GetAll(playlist);
        }
    }
}
