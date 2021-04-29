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
			content.Categories = GetStoredCategories(content.Categories);
			content.PlayLists = GetStoredPlaylists(content.PlayLists);
						
			this.contentRepository.Add(content);
		}

		private List<Category> GetStoredCategories(IEnumerable<Category> inMemoryCategories)
		{
			if (inMemoryCategories == null || inMemoryCategories.Count() == 0)
				throw new MissingCategoriesException();

			List<Category> storedCategories = inMemoryCategories.Select(category =>
			{
				Category stored = this.categoryRepository.Get(category.Id);
				if (stored != null)
					return stored;
				throw new NotFoundException(category.Id.ToString());
			}).ToList();
			
			return storedCategories;
		}

		private List<Playlist> GetStoredPlaylists(IEnumerable<Playlist> inMemoryPlaylists)
		{
			List<Playlist> storedPlaylists = new List<Playlist>();
			if (inMemoryPlaylists  != null)
			{
				storedPlaylists = inMemoryPlaylists.Select(playlist =>
				{
					Playlist stored;
					try
					{
						stored = this.playlistRepository.Get(playlist.Id);
					}
					catch (NotFoundException)
					{
						stored = null;
					}

					if (stored != null)
						return stored;
					if (!string.IsNullOrEmpty(playlist.Name))
						return playlist;

					throw new UnableToCreatePlaylistException();
				}).ToList();
			}
			return storedPlaylists;
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
