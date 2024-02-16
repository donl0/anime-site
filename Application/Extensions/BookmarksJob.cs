using Application.Models;
using Domain.Models.User;
using Mapster;
using System.Collections.Generic;

namespace Application.Extensions
{
	public static class BookmarksJob
	{
		public static AnimeFullVM FillBookmarks(this AnimeFullVM animeWM, ICollection<Bookmark> bookmarks, int animeId)
		{
			foreach (Bookmark bookmark in bookmarks)
			{
				BookmariInAnimePageVM bookmarkFull = bookmark.Adapt<BookmariInAnimePageVM>();

				if (bookmark.Animes != null)
					bookmarkFull.IsAnimeInBookmark = bookmark.Animes.Contains(animeId);

				animeWM.Bookmarks.Add(bookmarkFull);
			}

			return animeWM;
		}
	}
}
