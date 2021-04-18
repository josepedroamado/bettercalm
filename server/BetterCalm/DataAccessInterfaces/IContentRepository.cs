using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IContentRepository
	{
		Content Get(int id);
		IEnumerable<Content> GetAll();
		IEnumerable<Content> GetByCategory(Category category);
		IEnumerable<Content> GetByPlaylist(Playlist playlist);
		void Add(Content content);
		void Update(Content content);
	}
}
