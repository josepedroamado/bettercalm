using Domain;

namespace Model
{
	public class PlaylistBasicInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }

		public PlaylistBasicInfo() {}

		public PlaylistBasicInfo(Playlist playlist)
		{
			this.Id = playlist.Id;
			this.Name = playlist.Name;
			this.Description = playlist.Description;
			this.ImageUrl = playlist.ImageUrl;
		}

		public Playlist ToEntity()
		{
			return new Playlist()
			{
				Id = this.Id,
				Name = this.Name,
				Description = this.Description,
				ImageUrl = this.ImageUrl
			};
		}
	}
}
