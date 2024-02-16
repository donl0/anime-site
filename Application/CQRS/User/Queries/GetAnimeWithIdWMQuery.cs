using Application.Models;
using MediatR;

namespace Application.CQRS.User.Queries
{
	public class GetAnimeWithIdWMQuery : IRequest<AnimeFullVM>
	{
		public int AnimeId { get; set; }
		public string UserId { get; set; }
	}
}
