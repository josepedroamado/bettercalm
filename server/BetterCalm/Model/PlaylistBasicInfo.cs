using Domain;
using System;

namespace Model
{
	public class PlaylistBasicInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public PlaylistBasicInfo() {}

		public PlaylistBasicInfo(Playlist playlist)
		{
			this.Id = playlist.Id;
			this.Name = playlist.Name;
		}

		public override bool Equals(object obj)
		{
			if (obj is PlaylistBasicInfo movie)
				return this.Id == movie.Id && this.Name.Equals(movie.Name);

			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Name);
		}
	}
}
