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

		public Content ToEntity()
		{
			TimeSpan timeSpan = ContentLength == null ? 
				TimeSpan.Zero : TimeSpan.Parse(ContentLength);
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
				ContentType = new ContentType()
				{
					Name = ContentType
				}
			};
		}
	}
}
