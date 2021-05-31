using Domain;
using System;

namespace Model
{
	public class ContentBasicInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public TimeSpan ContentLength { get; set; }
		public string ArtistName { get; set; }
		public string ImageUrl { get; set; }
		public string ContentUrl { get; set; }
		public string ContentType { get; set; }

		public ContentBasicInfo(Content content)
		{
			this.Id = content.Id;
			this.Name = content.Name;
			this.ContentLength = content.ContentLength;
			this.ArtistName = content.ArtistName;
			this.ImageUrl = content.ImageUrl;
			this.ContentUrl = content.ContentUrl;
			this.ContentType = content.ContentType.Name;
		}
	}
}
