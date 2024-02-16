using MediatR;

namespace Application.CQRS.User.Commands
{
	public class AddBaseBookmarksCommand : IRequest<bool>
	{
		public string UserId { get; set; }
	}
}
