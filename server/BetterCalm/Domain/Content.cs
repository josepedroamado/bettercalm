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
		public string AudioUrl { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Playlist> PlayLists { get; set; }

		public bool Validate()
		{
			bool isValid = true;
			if (string.IsNullOrEmpty(this.ArtistName))
				throw new InvalidInputException("Artist name is required");
			if (string.IsNullOrEmpty(this.AudioUrl))
				throw new InvalidInputException("Audio Url is required");
			if (string.IsNullOrEmpty(this.Name))
				throw new InvalidInputException("Name is required");
			if (this.ContentLength == TimeSpan.Zero)
				throw new InvalidInputException("Content length is required");
			if (this.PlayLists != null)
				isValid = !this.PlayLists.Any(playlist => !playlist.Validate());

			return isValid;
		}

		public void UpdateFromContent(Content content)
		{
			this.Id = content.Id;

			if (!string.IsNullOrEmpty(content.ArtistName))
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
					Equals(this.AudioUrl, yContent.AudioUrl) &&
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
				this.AudioUrl, 
				this.Categories, 
				this.ContentLength, 
				this.Id, 
				this.ImageUrl, 
				this.Name, 
				this.PlayLists);
		}
	}
}
