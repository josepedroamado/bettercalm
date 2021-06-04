using ImporterInterfaces;
using ImporterModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImporterJSON.Test
{
	[TestClass]
	public class ImporterTest
	{
		[TestMethod]
		public void GetId_IdIsOk()
		{
			string expectedId = "JSON";
			IImporter importer = new Importer();
			Assert.AreEqual(expectedId, importer.GetId());
		}

		[TestMethod]
		public void Import_IsOk()
		{
			PlaylistImport playlist = new PlaylistImport()
			{
				Name = "Trending",
				Description = "Trending music",
				Categories = new int[] { 1002 }
			};
			ContentImport content = new ContentImport()
			{
				Name = "Beat It extended",
				ContentLength = "00:06:30",
				ArtistName = "Michael Jackson",
				ImageUrl = "http://images.com/image3.jpg",
				ContentUrl = "http://audio.com/audio3.mp3",
				Categories = new int[] { 1002 },
				ContentType = "audio",
				Playlists = new PlaylistImport[] { playlist }
			};
			List<ContentImport> expectedContents = new List<ContentImport>()
			{
				content
			};
			string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(basePath, "importTest.json");
			
			IImporter importer = new Importer();
			IEnumerable<ContentImport> imported = importer.Import(filePath);

			Assert.IsTrue(imported.SequenceEqual(expectedContents));
		}
	}
}
