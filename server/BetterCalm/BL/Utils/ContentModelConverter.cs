using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Utils
{
	public class ContentModelConverter
	{
		public static Content GetDomainContent(ImporterModel.Content content)
		{
			Content domainContent = new Content()
			{
				Id = content.Id,
				ArtistName = content.ArtistName,
				Categories = GetDomainCategories(content.Categories),
				ContentLength = TimeSpan.Parse(content.ContentLength),
				ContentType = new ContentType()
				{
					Name = content.ContentType
				},
				ContentUrl = content.ContentUrl,
				ImageUrl = content.ImageUrl,
				Name = content.Name,
				PlayLists = GetDomainPlaylists(content.Playlists)
			};

			

			return domainContent;
		}

		private static List<Category> GetDomainCategories(int[] categories)
		{
			List<Category> domainCategories = new List<Category>();
			categories.ToList().ForEach(category =>
				domainCategories.Add(new Category()
				{
					Id = category
				}));
			return domainCategories;
		}

		private static List<Playlist> GetDomainPlaylists(ImporterModel.Playlist[] playlists)
		{
			List<Playlist> domainPlaylists = new List<Playlist>();

			playlists.ToList().ForEach(playlist =>
				domainPlaylists.Append(new Playlist()
				{
					Id = playlist.Id,
					Categories = GetDomainCategories(playlist.Categories),
					Description = playlist.Description,
					ImageUrl = playlist.ImageUrl,
					Name = playlist.Name
				}));

			return domainPlaylists;
		}
	}
}
