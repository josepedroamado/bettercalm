using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IContentPlayer
	{
		public IEnumerable<Content> GetContents();
	}
}
