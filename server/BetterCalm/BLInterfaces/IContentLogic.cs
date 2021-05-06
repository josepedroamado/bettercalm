using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IContentLogic
	{
		public Content GetContent(int id);
		public IEnumerable<Content> GetContents();
		public IEnumerable<Content> GetContents(Playlist playlist);
		public IEnumerable<Content> GetContents(Category category);
		void CreateContent(Content content);
		void DeleteContent(int id);
		void UpdateContent(Content content);
	}
}
