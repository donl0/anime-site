using Application.Extensions;
using Application.Interfaces;
using Application.Models;
using Domain.Models.Shiki;
using Domain.Models.User;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Mapper.Mapster
{
	public class AnimeMapperMapster : IAnimeMapper
    {
        private readonly IMapper _mapper;

        public AnimeMapperMapster(IMapper mapper)
        {
            _mapper = mapper;
		}

		public async Task<AnimeFullVM> Map(User user, int animeId, AnimePageDTO animeDTO, List<Bookmark> bookmarks, Rating rating)
		{
			AnimeFullVM animeWM = animeDTO.Adapt<AnimeFullVM>();

			if (rating != null)
			{
				animeWM.UserRating = rating.value;
			}

			animeWM = animeWM.FillBookmarks(bookmarks, animeId);
			return animeWM;
		}

		public async Task<List<AnimePageVM>> Map(List<Anime> animes, IAnimeDbContext context, string userId)
		{
			List<AnimePageVM> animePageVMs = _mapper.Map<List<AnimePageVM>>(animes);

			List<Rating> ratings = await context.Ratings
			  .Where(r => r.User.Id == userId && animes.Select(a => a.Id).Contains(r.Anime))
			  .ToListAsync();

			foreach (var animePageVM in animePageVMs)
			{
				var rating = ratings.FirstOrDefault(r => r.Anime == animePageVM.Id);
				if (rating != null)
				{
					animePageVM.UserRating = rating.value;
				}
			}

			return animePageVMs;
		}
	}
}
