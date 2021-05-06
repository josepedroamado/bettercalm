using Model;
using System.Collections;

namespace WebAPI.Test
{
	public class ContentBasicInfoComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			ContentBasicInfo xContentBasicInfo = x as ContentBasicInfo;
			ContentBasicInfo yContentBasicInfo = y as ContentBasicInfo;

			if (xContentBasicInfo.Id == yContentBasicInfo.Id)
				return 0;
			return -1;
		}
	}
}
