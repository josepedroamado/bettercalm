using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IContentPlayer
	{
		IEnumerable<Category> GetCategories();
		Category GetCategory();
		IEnumerable<Playlist> GetPlaylists();
		Playlist GetPlaylist();
		IEnumerable<Content> GetContents();
		Content GetContent(int id);
		IEnumerable<Content> GetContent(Playlist playlist);
		IEnumerable<Content> GetContent(Category category);
		void CreateContent(Content content);
		void UpdateContent(Content content);
	}
}
