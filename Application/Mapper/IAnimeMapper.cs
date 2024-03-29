﻿using Application.Interfaces;
using Application.Models;
using Domain.Models.Shiki;
using Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public interface IAnimeMapper
    {
        public Task<AnimeFullVM> Map(User user, int animeId, AnimePageDTO animeDTO, List<Bookmark> bookmarks, Rating rating);

        public Task<List<AnimePageVM>> Map(List<Anime> animes, IAnimeDbContext context, string userId);

	}
}
