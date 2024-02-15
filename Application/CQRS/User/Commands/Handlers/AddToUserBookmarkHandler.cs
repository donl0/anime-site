using Application.CQRS.User.Commands;
using Application.CQRS.User.Commands.Handlers.Common;
using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.User.Commands.Handlers
{
    internal class AddToUserBookmarkHandler : UserBookmarkJobber, IRequestHandler<AddToUserBookmarkCommand, bool>
    {
        private readonly IAnimeDbContext _context;

        public AddToUserBookmarkHandler(IAnimeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddToUserBookmarkCommand request, CancellationToken cancellationToken)
        {
            Domain.Models.User.Bookmark bookmark = await TryTakeBookmark(_context, request.AnimeId, request.UserId, request.Bookmark);

            Domain.Models.Anime anime = new Domain.Models.Anime
            {
                Id = request.AnimeId
            };

            bookmark.Animes.Add(anime);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
