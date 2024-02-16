using Application.Common;
using Application.CQRS.Anime.Queries;
using Application.Interfaces;
using Application.Models;
using Domain.Models.User;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserPath = Domain.Models.User;
using Application.Extensions;
using Application.Mapper;
using MapsterMapper;
using Domain.Models.Shiki;

namespace Application.CQRS.User.Queries.Handlers
{
	public class GetAnimeWithIdWMHandler : IRequestHandler<GetAnimeWithIdWMQuery, AnimeFullVM>
	{
		private readonly IMediator _mediator;
		private readonly IAnimeDbContext _context;
		private readonly IAnimeMapper _mapper;

		public GetAnimeWithIdWMHandler(IMediator mediator, IAnimeDbContext context, IAnimeMapper mapper)
		{
			_mediator = mediator;
			_context = context;
			_mapper = mapper;
		}

		public async Task<AnimeFullVM> Handle(GetAnimeWithIdWMQuery request, CancellationToken cancellationToken)
		{
			GetAnimeWithIdQuery query = new GetAnimeWithIdQuery
			{
				AnimeId = request.AnimeId
			};

			AnimePageDTO animeDTO = await _mediator.Send(query);

			UserPath.User user = await GetUser(_context, request.UserId);

			List<Bookmark> bookmarks = await _context.Bookmarks.Where(b => b.User.Id == user.Id).ToListAsync();

			Rating rating = await _context.Ratings.FirstOrDefaultAsync(r => r.User == user & r.Anime == request.AnimeId);

			AnimeFullVM animeWM = await _mapper.Map(user, request.AnimeId, animeDTO, bookmarks, rating);

			return animeWM;
		}

		private async Task<UserPath.User> GetUser(IAnimeDbContext context, string userId) {

			UserPath.User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null)
				throw new NotFoundException(nameof(UserPath.User), userId);
			return user;
		}
	}
}
