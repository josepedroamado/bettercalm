using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IContentRepository
	{
		Content Get(int id);
		IEnumerable<Content> GetAll();
		IEnumerable<Content> GetAll(Category category);
		IEnumerable<Content> GetAll(Playlist playlist);
		void Add(Content content);
		void Update(Content content);
	}
}
