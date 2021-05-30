using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IContentRepository
	{
		Content Get(int id);
		IEnumerable<Content> GetAll();
		IEnumerable<Content> GetAll(Playlist playlist);
		IEnumerable<Content> GetAll(Category category);
		IEnumerable<Content> GetAll(string contentType);
		void Add(Content content);
		void Delete(int id);
		void Update(Content content);
	}
}
