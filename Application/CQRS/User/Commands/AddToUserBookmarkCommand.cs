using MediatR;

namespace Application.CQRS.User.Commands
{
	public class AddToUserBookmarkCommand: IRequest<bool>
	{
		public int UserId { get; set; }
		public int AnimeId { get; set; }
		public string Bookmark { get; set; }
	}
}
