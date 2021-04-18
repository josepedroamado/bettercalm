using System;
using System.Collections.Generic;

namespace Domain
{
	public class Content
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public TimeSpan ContentLength { get; set; }
		public string ArtistName { get; set; }
		public string ImageUrl { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Playlist> PlayLists { get; set; }
	}
}
