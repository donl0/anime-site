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
using Domain.Models.Shiki;

namespace Application.CQRS.User.Queries.Handlers
{
	public class GetAnimeWithIdWMHandler : IRequestHandler<GetAnimeWithIdWMQuery, AnimeFullVM>
	{
		private readonly IMediator _mediator;
		private readonly IAnimeDbContext _context;

		public GetAnimeWithIdWMHandler(IMediator mediator, IAnimeDbContext context)
		{
			_mediator = mediator;
			_context = context;
		}

		public async Task<AnimeFullVM> Handle(GetAnimeWithIdWMQuery request, CancellationToken cancellationToken)
		{
			GetAnimeWithIdQuery query = new GetAnimeWithIdQuery
			{
				AnimeId = request.AnimeId
			};

			AnimePageDTO animeDTO = await _mediator.Send(query);

			AnimeFullVM animeWM = animeDTO.Adapt<AnimeFullVM>();

			UserPath.User user = await GetUser(_context, request.UserId);

			List<Bookmark> bookmarks = await _context.Bookmarks.Where(b => b.User.Id == user.Id).ToListAsync();

			animeWM = await FillWithUserRating(_context, user, request.AnimeId, animeWM);

			animeWM = animeWM.FillBookmarks(bookmarks, request.AnimeId);

			return animeWM;
		}

		private async Task<UserPath.User> GetUser(IAnimeDbContext context, string userId) {

			UserPath.User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null)
				throw new NotFoundException(nameof(UserPath.User), userId);
			return user;
		}

		private async Task<AnimeFullVM> FillWithUserRating(IAnimeDbContext context, UserPath.User user, int animeId, AnimeFullVM animeWM) {
			Rating rating = await _context.Ratings.FirstOrDefaultAsync(r => r.User == user & r.Anime == animeId);

			if (rating != null) {
				animeWM.UserRating = rating.value;
			}

			return animeWM;
		}
	}
}
