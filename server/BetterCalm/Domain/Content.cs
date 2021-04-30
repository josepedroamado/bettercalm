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
			this.Id = content.Id;

			if (!string.IsNullOrEmpty(content.Name))
				this.ArtistName = content.ArtistName;
			if (!string.IsNullOrEmpty(content.AudioUrl))
				this.AudioUrl = content.AudioUrl;
			if (content.Categories != null)
				this.Categories = content.Categories;
			if (content.ContentLength != null)
				this.ContentLength = content.ContentLength;
			if (!string.IsNullOrEmpty(content.ImageUrl))
				this.ImageUrl = content.ImageUrl;
			if (!string.IsNullOrEmpty(content.Name))
				this.Name = content.Name;
			if (content.PlayLists != null)
				this.PlayLists = content.PlayLists;
		}
	}
}
