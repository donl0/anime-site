using MediatR;

namespace Application.CQRS.User.Queries
{
	public class IsAnimeInBookmarkQuery : IRequest<bool>
	{
		public string UserId { get; set; }
		public string Bookmark { get; set; }
		public int AnimeId { get; set; }
	}
}
