using Application.Common;
using Application.Interfaces;
using Application.Models;
using Domain.Models.User;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserPath = Domain.Models.User;

namespace Application.CQRS.User.Queries.Handlers
{
	public class GetUserBookmarksHandler : IRequestHandler<GetUserBookmarksQuery, ICollection<UserBookmarkVM>>
    {
        private readonly IAnimeDbContext _context;
        private readonly IMapper _mapper;

        public GetUserBookmarksHandler(IAnimeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<UserBookmarkVM>> Handle(GetUserBookmarksQuery request, CancellationToken cancellationToken)
        {
            UserPath.User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
                throw new NotFoundException(nameof(UserPath.User), request.UserId);

            ICollection<Bookmark> bookmarks = await _context.Bookmarks.Where(b => b.User == user).ToListAsync();

            ICollection<UserBookmarkVM> bookmarksVM = _mapper.Map<ICollection<UserBookmarkVM>>(bookmarks);

            return bookmarksVM;
        }
    }
}
