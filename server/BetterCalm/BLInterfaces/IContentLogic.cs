using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IContentLogic
	{
		public IEnumerable<Content> GetContents();
	}
}
