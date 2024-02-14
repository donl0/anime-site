using Application.CQRS.Anime.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimeListSite.Controllers
{
    public class AnimesController : Controller
    {
		private readonly IMediator _mediator;

		public AnimesController(IMediator mediator)
		{
			_mediator = mediator;
		}

        public async Task<IActionResult> AnimesAsync(string name) { 
            GetAnimesWithNameQuery query = new GetAnimesWithNameQuery { 
            Name = name 
            };

            List<AnimePageVM> animes = await _mediator.Send(query);

			return View("Base/AnimeList", animes);
		}

		public async Task<IActionResult> AnimeAsync(int id) {
            GetAnimeWithIdQuery query = new GetAnimeWithIdQuery
            {
                AnimeId = id
            };

            AnimeFullVM anime = await _mediator.Send(query);

            return View(anime);
        }

		public async Task<IActionResult> TopHundredAsync()
        {
            GetTopHundredAnimesQuery query = new GetTopHundredAnimesQuery();

            List<AnimePageVM> animes = await _mediator.Send(query);

            return View("Base/AnimeList", animes);
        }
    }
}
