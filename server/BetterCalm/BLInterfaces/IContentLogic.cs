using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IContentLogic
	{
		public IEnumerable<Content> GetContents();
		public IEnumerable<Content> GetContents(Playlist playlist);
		public Content GetContent(int id);
	}
}
