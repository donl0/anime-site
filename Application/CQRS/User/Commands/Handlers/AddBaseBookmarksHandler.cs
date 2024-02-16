using Application.Common;
using Application.CQRS.User.Commands;
using Application.Interfaces;
using Domain.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.User.Commands.Handlers
{
    public class AddBaseBookmarksHandler : IRequestHandler<AddBaseBookmarksCommand, bool>
    {
        private readonly IAnimeDbContext _context;

        public AddBaseBookmarksHandler(IAnimeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddBaseBookmarksCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
                throw new NotFoundException(nameof(Domain.Models.User.User), request.UserId);

            const string watched = "Watched";
            const string planned = "Planned";
            const string abandoned = "Abandoned";

            CancellationToken cancellation = new CancellationToken();

            await CreateBookmark(watched, user, _context, cancellation);
            await CreateBookmark(planned, user, _context, cancellation);
            await CreateBookmark(abandoned, user, _context, cancellation);

            return true;
        }

        private async Task CreateBookmark(string name, Domain.Models.User.User user, IAnimeDbContext context, CancellationToken cancellationToken)
        {
            var bookmark = new Bookmark
            {
                Title = name,
                User = user,
            };

            await context.Bookmarks.AddAsync(bookmark);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
