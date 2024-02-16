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

			Rating rating = new Rating
			{
				User = user,
				value = request.Rating,
				Anime = request.AnimeId
			};

			await _context.Ratings.AddAsync(rating);

			await _context.SaveChangesAsync(cancellationToken);

			return request.Rating;
        }
	}
}
