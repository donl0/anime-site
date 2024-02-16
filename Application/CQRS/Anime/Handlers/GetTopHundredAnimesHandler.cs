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
	public class GetTopHundredAnimesHandler : IRequestHandler<GetTopHundredAnimesQuery, List<AnimePageVM>>
    {
        private readonly IAnimeClient _client;
        private readonly IAnimeMapper _mapper;
        private readonly IAnimeDbContext _context;

		public GetTopHundredAnimesHandler(IAnimeClient client, IAnimeMapper mapper, IAnimeDbContext context)
		{
			_client = client;
			_mapper = mapper;
			_context = context;
		}

		public async Task<List<AnimePageVM>> Handle(GetTopHundredAnimesQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Models.Shiki.Anime> animes = await _client.GetTopHundredAsync();

			List<AnimePageVM> animesVm = await _mapper.Map(animes, _context, request.UserId);

			return animesVm;
        }
    }
}
