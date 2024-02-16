using Application.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.CQRS.User.Queries
{
	public class GetAnimesFromBookmarkQuery :IRequest<ICollection<AnimePageVM>>
	{
		public string UserId { get; set; }
		public string BookmarkName { get; set; }
	}
}
