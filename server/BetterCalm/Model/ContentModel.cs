using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class ContentModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ContentLength { get; set; }
		public string ArtistName { get; set; }
		public string ImageUrl { get; set; }
		public string ContentUrl { get; set; }
		public int[] Categories { get; set; }
		public string ContentType { get; set; }
		public IEnumerable<PlaylistBasicInfo> Playlists {get; set;}

		public ContentModel() { }

		public ContentModel(Content content)
		{
			this.Id = content.Id;
			this.Name = content.Name;
			this.ContentLength = content.ContentLength.ToString();
			this.ArtistName = content.ArtistName;
			this.ImageUrl = content.ImageUrl;
			this.ContentUrl = content.ContentUrl;
			this.ImageUrl = content.ImageUrl;
			this.Categories = content.Categories?.Select(category => category.Id).ToArray();
			this.ContentType = content.ContentType.Name;
			this.Playlists = content.PlayLists?.Select(playlist => new PlaylistBasicInfo(playlist));
		}

		public Content ToEntity()
		{
			TimeSpan timeSpan = ContentLength == null ? 
				TimeSpan.Zero : TimeSpan.Parse(ContentLength);
			ContentType contentType = !string.IsNullOrEmpty(ContentType) ? 
				new ContentType()
				{
					Name = ContentType
				} :
				null;

			return new Content()
			{
				ArtistName = ArtistName,
				ContentUrl = ContentUrl,
				Categories = Categories?.Select(category =>
					new Category() { Id = category }).ToList(),
				ContentLength = timeSpan,
				Id = Id,
				ImageUrl = ImageUrl,
				Name = Name,
				PlayLists = Playlists?.Select(playlist => playlist.ToEntity()),
				ContentType = contentType
			};
		}
	}
}
