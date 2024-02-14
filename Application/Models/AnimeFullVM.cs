using Domain.Models.Shiki;

namespace Application.Models
{
	public class AnimeFullVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Image Image { get; set; }
		public Genre[] Genres { get; set; }
		public string Score { get; set; }
		public int Episodes { get; set; }
        public string Kind { get; set; }
		public string Description { get; set; }
	}
}
