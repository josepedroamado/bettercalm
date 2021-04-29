using System.Collections.Generic;

namespace Model
{
	public class ContentModel
	{
		public string Name { get; set; }
		public string ContentLength { get; set; }
		public string ArtistName { get; set; }
		public string ImageURL { get; set; }
		public string AudioURL { get; set; }
		public int[] Categories { get; set; }
		public IEnumerable<PlaylistBasicInfo> Playlists {get; set;}
	}
}
