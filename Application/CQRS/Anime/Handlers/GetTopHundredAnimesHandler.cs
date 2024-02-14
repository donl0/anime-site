using Application.CQRS.Anime.Queries;
using Application.Interfaces;
using Application.Models;
using MapsterMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Anime.Handlers
{
	public class GetTopHundredAnimesHandler : IRequestHandler<GetTopHundredAnimesQuery, List<AnimePageVM>>
    {
        private readonly IAnimeClient _client;
        private readonly IMapper _mapper;

        public GetTopHundredAnimesHandler(IAnimeClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<List<AnimePageVM>> Handle(GetTopHundredAnimesQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Models.Shiki.Anime> animes = await _client.GetTopHundredAsync();

            List<AnimePageVM> animesVm = _mapper.Map<List<AnimePageVM>>(animes);

            return animesVm;
        }
    }
}
