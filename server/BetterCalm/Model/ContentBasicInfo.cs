using Domain;
using System;
using System.Linq;

namespace Model
{
	public class ContentBasicInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public TimeSpan ContentLength { get; set; }
		public string ArtistName { get; set; }
		public string ImageUrl { get; set; }
		public string ContentUrl { get; set; }
		public string ContentType { get; set; }
		public int[] Categories { get; set; }

		public ContentBasicInfo(Content content)
		{
			this.Id = content.Id;
			this.Name = content.Name;
			this.ContentLength = content.ContentLength;
			this.ArtistName = content.ArtistName;
			this.ImageUrl = content.ImageUrl;
			this.ContentUrl = content.ContentUrl;
			this.ContentType = content.ContentType.Name;
			
			if (content.Categories != null)
			{
				this.Categories = new int[content.Categories.Count()];
				int index = 0;
				foreach (Category category in content.Categories)
				{
					this.Categories[index] = category.Id;
					index++;
				}
			}
		}
	}
}