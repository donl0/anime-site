using Domain.Models.Shiki;

namespace Application.Models
{
	public class AnimePageVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Image Image { get; set; }
		public string Score { get; set; }
		public int UserRating { get; set; }
	}
}
