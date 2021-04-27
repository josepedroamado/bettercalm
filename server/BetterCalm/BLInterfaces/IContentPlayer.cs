using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IContentPlayer
	{
		public IEnumerable<Playlist> GetPlaylists();

		public Playlist GetPlaylist(int id);

		public IEnumerable<Category> GetCategories();

		public Category GetCategory(int Id);

		public IEnumerable<Content> GetContents();
	}
}
