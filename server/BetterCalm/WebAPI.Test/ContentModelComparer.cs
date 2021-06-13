using Model;
using System.Collections;

namespace WebAPI.Test
{
	public class ContentModelComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			ContentModel xContentBasicInfo = x as ContentModel;
			ContentModel yContentBasicInfo = y as ContentModel;

			if (xContentBasicInfo.Id == yContentBasicInfo.Id)
				return 0;
			return -1;
		}
	}
}
