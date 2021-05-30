using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
	public class Content
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public TimeSpan ContentLength { get; set; }
		public string ArtistName { get; set; }
		public string ImageUrl { get; set; }
		public string ContentUrl { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Playlist> PlayLists { get; set; }
		public ContentType ContentType { get; set; }

		public bool Validate()
		{
			bool isValid = true;
			if (string.IsNullOrEmpty(this.ArtistName))
				throw new InvalidInputException("Artist name is required");
			if (string.IsNullOrEmpty(this.ContentUrl))
				throw new InvalidInputException("Content Url is required");
			if (string.IsNullOrEmpty(this.Name))
				throw new InvalidInputException("Name is required");
			if (this.ContentLength == TimeSpan.Zero)
				throw new InvalidInputException("Content length is required");
			if (this.PlayLists != null)
				isValid = !this.PlayLists.Any(playlist => !playlist.Validate());
			if(this.ContentType == null)
				throw new InvalidInputException("Content type is required");

			return isValid;
		}

		public void UpdateFromContent(Content content)
		{
			this.Id = content.Id;

			if (!string.IsNullOrEmpty(content.ArtistName))
				this.ArtistName = content.ArtistName;
			if (!string.IsNullOrEmpty(content.ContentUrl))
				this.ContentUrl = content.ContentUrl;
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

		public override bool Equals(object obj)
		{
			if (obj is Content yContent)
			{
				bool equalsPlaylist;
				if (this.PlayLists != null && yContent.PlayLists != null)
					equalsPlaylist = this.PlayLists.SequenceEqual(yContent.PlayLists);
				else
					equalsPlaylist = true;

				bool equalsCategories;
				if (this.Categories != null && yContent.Categories != null)
					equalsCategories = this.Categories.SequenceEqual(yContent.Categories);
				else
					equalsCategories = true;

				return Equals(this.ArtistName, yContent.ArtistName) &&
					Equals(this.ContentUrl, yContent.ContentUrl) &&
					Equals(ContentLength, yContent.ContentLength) &&
					this.Id == yContent.Id &&
					Equals(this.ImageUrl, yContent.ImageUrl) &&
					Equals(this.Name, yContent.Name) &&
					equalsPlaylist && equalsCategories;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.ArtistName, 
				this.ContentUrl, 
				this.Categories, 
				this.ContentLength, 
				this.Id, 
				this.ImageUrl, 
				this.Name, 
				this.PlayLists);
		}
	}
}
