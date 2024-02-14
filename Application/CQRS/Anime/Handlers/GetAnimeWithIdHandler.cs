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
	public class GetAnimeWithIdHandler : IRequestHandler<GetAnimeWithIdQuery, AnimeFullVM>
    {
		private readonly IAnimeClient _client;
		private readonly IMapper _mapper;

		public GetAnimeWithIdHandler(IAnimeClient client, IMapper mapper)
		{
			_client = client;
			_mapper = mapper;
		}

		public async Task<AnimeFullVM> Handle(GetAnimeWithIdQuery request, CancellationToken cancellationToken)
        {
			AnimeId anime = await _client.GetFullInfoWithIdAsync(request.AnimeId);

			AnimeFullVM vm = _mapper.Map<AnimeFullVM>(anime);

			return vm;
        }
    }
}
