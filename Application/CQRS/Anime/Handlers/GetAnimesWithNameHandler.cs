using Application.CQRS.Anime.Queries;
using Application.Interfaces;
using Application.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Anime.Handlers
{
	public class GetAnimesWithNameHandler : IRequestHandler<GetAnimesWithNameQuery, List<AnimePageVM>>
	{
		private readonly IAnimeClient _client;
		private readonly IMapper _mapper;

		public GetAnimesWithNameHandler(IAnimeClient client, IMapper mapper)
		{
			_client = client;
			_mapper = mapper;
		}

		public async Task<List<AnimePageVM>> Handle(GetAnimesWithNameQuery request, CancellationToken cancellationToken)
		{
			var animes = await _client.GetWithNameAsync(request.Name);

			List<AnimePageVM> vm = _mapper.Map<List<AnimePageVM>>(animes);

			return vm;
		}
	}
}
