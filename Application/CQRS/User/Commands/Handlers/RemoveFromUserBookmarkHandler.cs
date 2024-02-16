using Application.CQRS.User.Commands;
using Application.CQRS.User.Commands.Handlers.Common;
using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.User.Commands.Handlers
{
    public class RemoveFromUserBookmarkHandler : UserBookmarkJobber, IRequestHandler<RemoveFromUserBookmarkCommand, bool>
    {
        private readonly IAnimeDbContext _context;

        public RemoveFromUserBookmarkHandler(IAnimeDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(RemoveFromUserBookmarkCommand request, CancellationToken cancellationToken)
        {
            Domain.Models.User.Bookmark bookmark = await TryTakeBookmark(_context, request.AnimeId, request.UserId, request.Bookmark);


            bookmark.Animes.Remove(request.AnimeId);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
