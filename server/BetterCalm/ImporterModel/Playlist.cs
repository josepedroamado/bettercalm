using System;
using System.Linq;

namespace ImporterModel
{
	public class Playlist
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public int[] Categories { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is Playlist playlistImportObject)
			{
				return Equals(Id, playlistImportObject.Id) &&
					Equals(Name, playlistImportObject.Name) &&
					Equals(Description, playlistImportObject.Description) &&
					Equals(ImageUrl, playlistImportObject.ImageUrl) &&
					Categories.SequenceEqual(playlistImportObject.Categories);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Id,
				this.ImageUrl,
				this.Name,
				this.Categories,
				this.Description);
		}
	}
}
