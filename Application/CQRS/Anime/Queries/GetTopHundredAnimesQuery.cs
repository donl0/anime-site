using Application.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.CQRS.Anime.Queries
{
	public class GetTopHundredAnimesQuery : IRequest<List<AnimePageVM>>
    {
    }
}
