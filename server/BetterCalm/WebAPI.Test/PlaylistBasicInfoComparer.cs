using Model;
using System.Collections;

namespace WebAPI.Test
{
	class PlaylistBasicInfoComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			PlaylistBasicInfo xPlaylistBasicInfo = x as PlaylistBasicInfo;
			PlaylistBasicInfo yPlaylistBasicInfo = y as PlaylistBasicInfo;

			if (xPlaylistBasicInfo.Id == yPlaylistBasicInfo.Id &&
				xPlaylistBasicInfo.Name == yPlaylistBasicInfo.Name)
				return 0;
			return -1;
		}
	}
}
