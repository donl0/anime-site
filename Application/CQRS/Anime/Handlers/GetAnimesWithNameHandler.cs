using Application.CQRS.Anime.Queries;
using Application.Interfaces;
using Application.Mapper;
using Application.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Anime.Handlers
{
	public class GetAnimesWithNameHandler : IRequestHandler<GetAnimesWithNameQuery, List<AnimePageVM>>
	{
		private readonly IAnimeClient _client;
		private readonly IAnimeMapper _mapper;
		private readonly IAnimeDbContext _context;

		public GetAnimesWithNameHandler(IAnimeClient client, IAnimeMapper mapper, IAnimeDbContext context)
		{
			_client = client;
			_mapper = mapper;
			_context = context;
		}

		public async Task<List<AnimePageVM>> Handle(GetAnimesWithNameQuery request, CancellationToken cancellationToken)
		{
			var animes = await _client.GetWithNameAsync(request.Name);


			List<AnimePageVM> animesVm = await _mapper.Map(animes, _context, request.UserId);

			return animesVm;
		}
	}
}
