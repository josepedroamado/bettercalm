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
	}
}
