using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IContentPlayer
	{
		public IEnumerable<Playlist> GetPlaylists();

		public IEnumerable<Category> GetCategories();
	}
}
