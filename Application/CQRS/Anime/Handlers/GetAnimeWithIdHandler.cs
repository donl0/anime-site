using Application.CQRS.Anime.Queries;
using Application.Interfaces;
using Application.Models;
using Domain.Models.Shiki;
using MapsterMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Anime.Handlers
{
	public class GetAnimeWithIdHandler : IRequestHandler<GetAnimeWithIdQuery, Models.AnimePageDTO>
    {
		private readonly IAnimeClient _client;
		private readonly IMapper _mapper;

		public GetAnimeWithIdHandler(IAnimeClient client, IMapper mapper)
		{
			_client = client;
			_mapper = mapper;
		}

		public async Task<Models.AnimePageDTO> Handle(GetAnimeWithIdQuery request, CancellationToken cancellationToken)
        {
			Domain.Models.Shiki.AnimeFullDTO anime = await _client.GetFullInfoWithIdAsync(request.AnimeId);

			Models.AnimePageDTO vm = _mapper.Map<Models.AnimePageDTO>(anime);

			return vm;
        }
    }
}
