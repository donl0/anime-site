using Application.Common;
using Application.Interfaces;
using Application.Models;
using Domain.Models.Shiki;
using Domain.Models.User;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserPath = Domain.Models.User;
using AnimePath = Domain.Models.Shiki;
using MapsterMapper;

namespace Application.CQRS.User.Queries.Handlers
{
	public class GetAnimesFromBookmarkHandler : IRequestHandler<GetAnimesFromBookmarkQuery, ICollection<AnimePageVM>>
	{
		private readonly IAnimeDbContext _context;
		private readonly IAnimeClient _client;
		private readonly IMapper _mapper;

		public GetAnimesFromBookmarkHandler(IAnimeDbContext context, IAnimeClient client, IMapper mapper)
		{
			_context = context;
			_client = client;
			_mapper = mapper;
		}

		public async Task<ICollection<AnimePageVM>> Handle(GetAnimesFromBookmarkQuery request, CancellationToken cancellationToken)
		{
			UserPath.User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

			if (user == null)
				throw new NotFoundException(nameof(UserPath.User), request.UserId);

			Bookmark bookmark = await _context.Bookmarks.FirstOrDefaultAsync(b => b.User == user & b.Title == request.BookmarkName);

			if (bookmark == null)
				throw new NotFoundException(nameof(Bookmark), $"with user id:{request.UserId}");


			ICollection<AnimePath.AnimeFullDTO> animesDTO = await ConvertFromBookmark(bookmark.Animes);

			ICollection<AnimePageVM> animesVM =  _mapper.Map<ICollection<AnimePageVM>>(animesDTO);

			return animesVM;
		}

		private async Task<ICollection<AnimePath.AnimeFullDTO>> ConvertFromBookmark(ICollection<int> animes) {
			if (animes == null)
				return new List<AnimePath.AnimeFullDTO>();

			ICollection<AnimePath.AnimeFullDTO> animesDTO = new List<Domain.Models.Shiki.AnimeFullDTO>();

			foreach (var animeBookmark in animes)
			{
				var anime = await _client.GetFullInfoWithIdAsync(animeBookmark);
				animesDTO.Add(anime);
			}

			return animesDTO;
		}
	}
}
