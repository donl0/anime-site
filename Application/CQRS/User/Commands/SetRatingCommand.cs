using MediatR;

namespace Application.CQRS.User.Commands
{
	public class SetRatingCommand : IRequest<int>
	{
		public int AnimeId { get; set; }
		public string UserId { get; set; }
		public int Rating { get; set; }
	}
}
