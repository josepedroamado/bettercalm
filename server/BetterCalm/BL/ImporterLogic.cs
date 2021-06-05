using BLInterfaces;
using System;

namespace BL
{
	public class ImporterLogic : IImporterLogic
	{
		private IContentLogic contentLogic;
		public ImporterLogic(IContentLogic contentLogic)
		{
			this.contentLogic = contentLogic;
		}

		public void Import(string type, string filePath)
		{
			throw new NotImplementedException();
		}
	}
}
