using Domain.Models.Shiki;
using System.Collections.Generic;

namespace Application.Models
{
	public class AnimePageDTO
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
