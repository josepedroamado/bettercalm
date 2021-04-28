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
				Category notFoundCategory = content.Categories.FirstOrDefault(category =>
				this.categoryRepository.Get(category.Id) == null);
				if (notFoundCategory != null)
					throw new NotFoundException(notFoundCategory.Id.ToString());
			}

			if (content.PlayLists != null)
			{
				Playlist unableToCreatePlaylist = content.PlayLists.FirstOrDefault(playlist =>
					this.playlistRepository.Get(playlist.Id) == null &&
					string.IsNullOrEmpty(playlist.Name)
				);

				if (unableToCreatePlaylist != null)
					throw new UnableToCreatePlaylistException();
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
