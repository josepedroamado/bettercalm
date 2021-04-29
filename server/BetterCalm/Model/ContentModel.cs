using Domain;
using System;
using System.Collections.Generic;

namespace Model
{
	public class ContentModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ContentLength { get; set; }
		public string ArtistName { get; set; }
		public string ImageUrl { get; set; }
		public string AudioUrl { get; set; }
		public int[] Categories { get; set; }
		public IEnumerable<PlaylistBasicInfo> Playlists {get; set;}

		public Content ToEntity()
		{
			throw new NotImplementedException();
		}
	}
}
