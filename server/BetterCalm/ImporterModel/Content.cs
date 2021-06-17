using System;
using System.Linq;

namespace ImporterModel
{
	public class Content
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ContentLength { get; set; }
		public string ArtistName { get; set; }
		public string ImageUrl { get; set; }
		public string ContentUrl { get; set; }
		public int[] Categories { get; set; }
		public string ContentType { get; set; }
		public Playlist[] Playlists { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is Content contentImportObject)
			{
				return Equals(this.ArtistName, contentImportObject.ArtistName) &&
					this.Categories.SequenceEqual(contentImportObject.Categories) &&
					Equals(this.ContentLength, contentImportObject.ContentLength) &&
					Equals(this.ContentType, contentImportObject.ContentType) &&
					Equals(this.ContentUrl, contentImportObject.ContentUrl) &&
					Equals(this.Id, contentImportObject.Id) &&
					Equals(this.ImageUrl, contentImportObject.ImageUrl) &&
					Equals(this.Name, contentImportObject.Name) &&
					this.Playlists.SequenceEqual(contentImportObject.Playlists);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.ArtistName,
				this.ContentLength,
				this.ContentType,
				this.ContentUrl,
				this.Id,
				this.ImageUrl,
				this.Name,
				this.Playlists);
		}
	}
}