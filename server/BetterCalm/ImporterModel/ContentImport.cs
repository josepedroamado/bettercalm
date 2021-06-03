using System.Collections.Generic;

namespace ImporterModel
{
	public class ContentImport
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ContentLength { get; set; }
		public string ArtistName { get; set; }
		public string ImageUrl { get; set; }
		public string ContentUrl { get; set; }
		public int[] Categories { get; set; }
		public string ContentType { get; set; }
		public IEnumerable<PlaylistImport> Playlists { get; set; }
	}
}