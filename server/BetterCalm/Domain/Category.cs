using System.Collections.Generic;

namespace Domain
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<Playlist> PlayLists { get; set; }
		public IEnumerable<Content> Contents { get; set; }
	}
}
