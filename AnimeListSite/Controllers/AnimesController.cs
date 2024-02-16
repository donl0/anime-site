using Application.CQRS.Anime.Queries;
using Application.CQRS.User.Queries;
using Application.Models;
using Domain.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AnimeListSite.Controllers
{
    public class AnimesController : Controller
    {
		private readonly IMediator _mediator;
		private readonly UserManager<User> _userManager;

		public AnimesController(IMediator mediator, UserManager<User> userManager)
		{
			_mediator = mediator;
			_userManager = userManager;
		}

		public async Task<IActionResult> AnimesAsync(string name) {
			var user = await _userManager.GetUserAsync(HttpContext.User);

			GetAnimesWithNameQuery query = new GetAnimesWithNameQuery { 
            Name = name,
            UserId = user.Id
            };

            List<AnimePageVM> animes = await _mediator.Send(query);

			return View("Base/AnimeList", animes);
		}

        [Authorize]
		public async Task<IActionResult> AnimeAsync(int id) {
			var user = await _userManager.GetUserAsync(HttpContext.User);

			GetAnimeWithIdWMQuery query = new GetAnimeWithIdWMQuery
            {
                AnimeId = id,
                UserId = user.Id
            };

            AnimeFullVM anime = await _mediator.Send(query);

            return View(anime);
        }

		public async Task<IActionResult> TopHundredAsync()
        {
			var user = await _userManager.GetUserAsync(HttpContext.User);

            GetTopHundredAnimesQuery query = new GetTopHundredAnimesQuery
            {
                UserId = user.Id
            };

            List<AnimePageVM> animes = await _mediator.Send(query);

            return View("Base/AnimeList", animes);
        }
    }
}
