namespace Domain.Models.Shiki
{
	public class Anime
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Russian { get; set; }
		public Image Image { get; set; }
		public string Url { get; set; }
		public string Kind { get; set; }
		public string Score { get; set; }
		public string Status { get; set; }
		public int Episodes { get; set; }
		public int EpisodesAired { get; set; }
		public string AiredOn { get; set; }
		public string ReleasedOn { get; set; }
	}
}
