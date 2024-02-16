using Application.Models;
using MediatR;

namespace Application.CQRS.User.Queries
{
	public class GetFirstBookmarkQuery : IRequest<UserBookmarkVM>
	{
		public string UserId { get; set; }
	}
}
