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
		public string AudioUrl { get; set; }
		public int[] Categories { get; set; }
		public IEnumerable<PlaylistBasicInfo> Playlists {get; set;}

		public Content ToEntity()
		{
			return new Content()
			{
				ArtistName = ArtistName,
				AudioUrl = AudioUrl,
				Categories = Categories.Select(category =>
					new Category() { Id = category }).ToList(),
				ContentLength = TimeSpan.Parse(ContentLength),
				Id = Id,
				ImageUrl = ImageUrl,
				Name = Name,
				PlayLists = Playlists.Select(playlist =>
					new Playlist()
					{
						Id = playlist.Id,
						Name = playlist.Name
					}).ToList(),
			};
		}
	}
}
