using System.Collections.Generic;

namespace Application.Models
{
	public class BookmarksWithAnimesVM
	{
		public ICollection<UserBookmarkVM> Bookmarks { get; set; }
		public ICollection<AnimePageVM> Animes { get; set; }
	}
}
