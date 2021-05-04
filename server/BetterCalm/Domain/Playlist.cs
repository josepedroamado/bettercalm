using Domain.Exceptions;
using System.Collections.Generic;

namespace Domain
{
	public class Playlist
	{
		public int Id { get; set;  }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Content> Contents { get; set; }

		public bool Validate()
		{
			if (string.IsNullOrEmpty(this.Name))
				throw new InvalidInputException("Playlist name is required");
			if (string.IsNullOrEmpty(this.Description))
				throw new InvalidInputException("Playlist description is required");

			return true;
		}
	}
}
