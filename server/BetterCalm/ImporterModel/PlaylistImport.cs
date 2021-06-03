namespace ImporterModel
{
	public class PlaylistImport
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public int[] Categories { get; set; }
	}
}
