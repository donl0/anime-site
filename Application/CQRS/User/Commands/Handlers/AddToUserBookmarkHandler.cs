using Application.CQRS.User.Commands.Handlers.Common;
using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.User.Commands.Handlers
{
	public class AddToUserBookmarkHandler : UserBookmarkJobber, IRequestHandler<AddToUserBookmarkCommand, bool>
    {
        private readonly IAnimeDbContext _context;

        public AddToUserBookmarkHandler(IAnimeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddToUserBookmarkCommand request, CancellationToken cancellationToken)
        {
			Domain.Models.User.Bookmark bookmark = await TryTakeBookmark(_context, request.AnimeId, request.UserId, request.Bookmark);

            if (bookmark.Animes == null)
                bookmark.Animes = new List<int>() { request.AnimeId };
            else
			    bookmark.Animes.Add(request.AnimeId);

			await _context.SaveChangesAsync(cancellationToken);

			return true;
        }
    }
}
