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
			if (content.Categories == null || content.Categories.Count() == 0)
				throw new MissingCategoriesException();

			content.Categories = GetStoredCategories(content.Categories);
			content.PlayLists = GetStoredPlaylists(content.PlayLists);
						
			this.contentRepository.Add(content);
		}

		private List<Category> GetStoredCategories(IEnumerable<Category> inMemoryCategories)
		{
			List<Category> storedCategories = new List<Category>();

			if (inMemoryCategories != null)
			{
				storedCategories = inMemoryCategories.Select(category =>
				{
					Category stored = this.categoryRepository.Get(category.Id);
					if (stored != null)
						return stored;
					throw new NotFoundException(category.Id.ToString());
				}).ToList();
			}
			
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
					{
						stored.Categories = GetStoredCategories(stored.Categories);
						return stored;
					}
						
					if (!string.IsNullOrEmpty(playlist.Name))
					{
						playlist.Categories = GetStoredCategories(playlist.Categories);
						return playlist;
					}	

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

        public IEnumerable<Content> GetContents(Category category)
        {
			return this.contentRepository.GetAll(category);
		}

		public void DeleteContent(int id)
		{
			this.contentRepository.Delete(id);
		}

		public void UpdateContent(Content content)
		{
			Content currentContent = this.contentRepository.Get(content.Id);
			if (currentContent == null)
				return;
			currentContent.UpdateFromContent(content);

			if (currentContent.Categories == null || currentContent.Categories.Count() == 0)
				throw new MissingCategoriesException();

			currentContent.PlayLists = GetStoredPlaylists(currentContent.PlayLists);
			currentContent.Categories = GetStoredCategories(currentContent.Categories);

			this.contentRepository.Update(currentContent);
		}
	}
}
