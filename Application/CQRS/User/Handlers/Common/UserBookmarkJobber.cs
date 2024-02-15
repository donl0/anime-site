using Application.Common;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.CQRS.User.Handlers.Common
{
	public class UserBookmarkJobber
	{
		protected async Task<Domain.Models.User.Bookmark> TryTakeBookmark(IAnimeDbContext context, int animeId, string userId, string bookmarkName) {

			var user = await context.Users.SingleOrDefaultAsync(u => u.Id == userId);

			if (user == null)
				throw new NotFoundException(nameof(User), bookmarkName);

			var bookmark = await context.Bookmarks.SingleOrDefaultAsync(b => b.Title == bookmarkName & b.User == user);

			if (bookmark == null)
				throw new NotFoundException(nameof(Domain.Models.User.Bookmark), bookmark);

			return bookmark;
		}
	}
}
