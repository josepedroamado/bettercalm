using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Domain
{
	public class ContentComparer : IEqualityComparer<Content>
	{
		public bool Equals([DisallowNull] Content xContent, [DisallowNull] Content yContent)
		{
			bool equalsPlaylist;
			if (xContent.PlayLists != null && yContent.PlayLists != null)
				equalsPlaylist = xContent.PlayLists.SequenceEqual(yContent.PlayLists);
			else
				equalsPlaylist = true;

			bool equalsCategories;
			if (xContent.Categories != null && yContent.Categories != null)
				equalsCategories = xContent.Categories.SequenceEqual(yContent.Categories);
			else
				equalsCategories = true;

			return xContent.ArtistName.Equals(yContent.ArtistName) &&
				xContent.AudioUrl.Equals(yContent.AudioUrl) &&
				xContent.ContentLength.Equals(yContent.ContentLength) &&
				xContent.Id == yContent.Id &&
				xContent.ImageUrl.Equals(yContent.ImageUrl) &&
				xContent.Name.Equals(yContent.Name) &&
				equalsPlaylist && equalsCategories;
		}

		public int GetHashCode([DisallowNull] Content obj)
		{
			return HashCode.Combine(obj.Name, obj.ArtistName, obj.AudioUrl);
		}
	}
}
