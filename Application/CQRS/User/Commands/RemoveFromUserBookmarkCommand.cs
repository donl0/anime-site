using MediatR;

namespace Application.CQRS.User.Commands
{
	public class RemoveFromUserBookmarkCommand : IRequest<bool>
	{
		public string UserId { get; set; }
		public string Bookmark;
		public int AnimeId { get; set; }
	}
}
