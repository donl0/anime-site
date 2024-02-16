using Application.Models;
using MediatR;

namespace Application.CQRS.Anime.Queries
{
    public class GetAnimeWithIdQuery : IRequest<AnimePageDTO>
    {
        public int AnimeId { get; set; }
    }
}
