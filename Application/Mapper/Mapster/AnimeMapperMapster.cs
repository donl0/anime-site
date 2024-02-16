using Application.Common;
using Application.Extensions;
using Application.Interfaces;
using Application.Models;
using Domain.Models.Shiki;
using Domain.Models.User;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

		public AnimePageVM Map(AnimeFullDTO anime, Rating rating)
		{
			throw new System.NotImplementedException();
		}
	}
}
