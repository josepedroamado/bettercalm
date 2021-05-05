using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class PlaylistBasicInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public int[] Categories { get; set; }

		public PlaylistBasicInfo() {}

		public PlaylistBasicInfo(Playlist playlist)
		{
			this.Id = playlist.Id;
			this.Name = playlist.Name;
			this.Description = playlist.Description;
			this.ImageUrl = playlist.ImageUrl;
			this.Categories = playlist.Categories?.Select(category => category.Id).ToArray();
		}

		public Playlist ToEntity()
		{
			List<Category> categories = new List<Category>();
			if (this.Categories != null)
				categories = this.Categories.Select(id => new Category() { Id = id }).ToList();
			
			return new Playlist()
			{
				Id = this.Id,
				Name = this.Name,
				Description = this.Description,
				ImageUrl = this.ImageUrl,
				Categories = categories
			};
		}
	}
}
