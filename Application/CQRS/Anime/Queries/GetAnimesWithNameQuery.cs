using Application.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.CQRS.Anime.Queries
{
	public class GetAnimesWithNameQuery : IRequest<List<AnimePageVM>> 
	{
		public string Name { get; set; }
		public string UserId { get; set; }
	}
}
