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
		public string AudioUrl { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Playlist> PlayLists { get; set; }

		public void UpdateFromContent(Content content)
		{
			this.ArtistName = content.ArtistName;
			this.AudioUrl = content.AudioUrl;
			this.Categories = content.Categories;
			this.ContentLength = content.ContentLength;
			this.Id = content.Id;
			this.ImageUrl = content.ImageUrl;
			this.Name = content.Name;
			this.PlayLists = content.PlayLists;
		}
	}
}
