using System.Collections.Generic;

namespace Application.Models
{
	public class AnimeFullVM : AnimePageDTO
	{
		public ICollection<BookmariInAnimePageVM> Bookmarks { get; set; } = new List<BookmariInAnimePageVM>();
		public int UserRating { get; set; }
	}
}
