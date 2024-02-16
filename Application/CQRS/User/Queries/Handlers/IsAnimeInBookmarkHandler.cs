using Application.Common;
using Application.Interfaces;
using Domain.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserPath = Domain.Models.User;

namespace Application.CQRS.User.Queries.Handlers
{
	public class IsAnimeInBookmarkHandler : IRequestHandler<IsAnimeInBookmarkQuery, bool>
	{
		private readonly IAnimeDbContext _context;
		public IsAnimeInBookmarkHandler(IAnimeDbContext context)
		{
			_context = context;
		}
		public async Task<bool> Handle(IsAnimeInBookmarkQuery request, CancellationToken cancellationToken)
		{
			UserPath.User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

			if (user == null)
				throw new NotFoundException(nameof(UserPath.User), request.UserId);

			Bookmark bookmark = await _context.Bookmarks.FirstOrDefaultAsync(b => b.User == user & b.Title == request.Bookmark);

			if (bookmark == null)
				throw new NotFoundException(nameof(UserPath.Bookmark), $"with user id{request.UserId} and {request.Bookmark}");

			if (bookmark.Animes==null || !bookmark.Animes.Contains(request.AnimeId))
				return false;
			
			return true;
		}
	}
}
