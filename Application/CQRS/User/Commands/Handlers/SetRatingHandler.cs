using Application.Common;
using Application.Interfaces;
using Domain.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserPath = Domain.Models.User;

namespace Application.CQRS.User.Commands.Handlers
{
	public class SetRatingHandler : IRequestHandler<SetRatingCommand, int>
	{
		private readonly IAnimeDbContext _context;

		public SetRatingHandler(IAnimeDbContext context)
		{
			_context = context;
		}

		public async Task<int> Handle(SetRatingCommand request, CancellationToken cancellationToken)
		{
			UserPath.User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

			if (user == null)
				throw new NotFoundException(nameof(UserPath.User), request.UserId);

			await ChangeOrAdd(_context, user, request.Rating, request.AnimeId);

			await _context.SaveChangesAsync(cancellationToken);

			return request.Rating;
        }

		private async Task ChangeOrAdd(IAnimeDbContext context, UserPath.User user, int ratingSet, int animeId) {

			Rating existRating = await _context.Ratings.FirstOrDefaultAsync(r => r.User == user & r.Anime == animeId);

			if (existRating == null)
			{
				Rating rating = new Rating
				{
					User = user,
					value = ratingSet,
					Anime = animeId
				};

				await _context.Ratings.AddAsync(rating);
			}
			else
			{
				existRating.value = ratingSet;
			}
		}
	}
}
