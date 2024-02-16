using Application.Interfaces;
using Application.Models;
using UserPath = Domain.Models.User;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Common;
using Domain.Models.User;
using Mapster;
using MapsterMapper;

namespace Application.CQRS.User.Queries.Handlers
{
	public class GetFirstBookmarkHandler : IRequestHandler<GetFirstBookmarkQuery, UserBookmarkVM>
    {
		private readonly IAnimeDbContext _context;
		private readonly IMapper _mapper;

		public GetFirstBookmarkHandler(IAnimeDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<UserBookmarkVM> Handle(GetFirstBookmarkQuery request, CancellationToken cancellationToken)
        {
			UserPath.User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

			if (user == null)
				throw new NotFoundException(nameof(UserPath.User), request.UserId);

			Bookmark bookmark = await _context.Bookmarks.FirstOrDefaultAsync(b => b.User == user);

			if (bookmark == null)
				throw new NotFoundException(nameof(Bookmark), $"with user id:{request.UserId}");

			UserBookmarkVM animes = _mapper.Map<UserBookmarkVM>(bookmark);

			return animes;
		}
    }
}
