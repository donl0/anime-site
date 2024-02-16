using Application.CQRS.User.Commands;
using Application.CQRS.User.Queries;
using Application.Models;
using Domain.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AnimeListSite.Controllers
{
	public class UserController : Controller
	{
		private readonly IMediator _mediator;
		private readonly UserManager<User> _userManager;

		public UserController(IMediator mediator, UserManager<User> userManager)
		{
			_mediator = mediator;
			_userManager = userManager;
		}

		[Authorize]
		public async Task<IActionResult> BookmarksFirstAsync()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);

			GetFirstBookmarkQuery firstBookmarkQuery = new GetFirstBookmarkQuery
			{
				UserId = user.Id
			};

			var firstBookmark = await _mediator.Send(firstBookmarkQuery);

			GetAnimesFromBookmarkQuery animesToShow = new GetAnimesFromBookmarkQuery
			{
				UserId = user.Id,
				BookmarkName = firstBookmark.Title
			};

			var animes = await _mediator.Send(animesToShow);

			GetUserBookmarksQuery AllBookmarksQuery = new GetUserBookmarksQuery
			{
				UserId = user.Id
			};

			var allBookmarks = await _mediator.Send(AllBookmarksQuery);

			BookmarksWithAnimesVM vm = new BookmarksWithAnimesVM
			{
				Animes = animes,
				Bookmarks = allBookmarks
			};

			return View("Bookmarks", vm);
		}

		public async Task<IActionResult> BookmarksAsync(string bookmarkName) {
			var user = await _userManager.GetUserAsync(HttpContext.User);

			GetAnimesFromBookmarkQuery animesQuery = new GetAnimesFromBookmarkQuery
			{
				UserId = user.Id,
				BookmarkName = bookmarkName
			};

			var animes = await _mediator.Send(animesQuery);

			GetUserBookmarksQuery query = new GetUserBookmarksQuery
			{
				UserId = user.Id
			};

			var bookmarks = await _mediator.Send(query);

			BookmarksWithAnimesVM vm = new BookmarksWithAnimesVM
			{
				Animes = animes,
				Bookmarks = bookmarks
			};

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> AddRemoveFromBookmarkAsync(string bookmark, int animeId) {
			var user = await _userManager.GetUserAsync(HttpContext.User);

			IsAnimeInBookmarkQuery query = new IsAnimeInBookmarkQuery
			{
				AnimeId = animeId,
				UserId = user.Id,
				Bookmark = bookmark
			};

			bool isInBookmark = await _mediator.Send(query);

			IRequest<bool> command = null;

			if (!isInBookmark)
			{
				command = new AddToUserBookmarkCommand
				{
					AnimeId = animeId,
					UserId = user.Id,
					Bookmark = bookmark
				};

			}
			else { 
				command = new RemoveFromUserBookmarkCommand
				{
					AnimeId = animeId,
					UserId = user.Id,
					Bookmark = bookmark
				};
			}

			await _mediator.Send(command);

			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> RatingAsync(int value, int animeId) {
			var user = await _userManager.GetUserAsync(HttpContext.User);

			SetRatingCommand command = new SetRatingCommand { Rating = value,
				UserId = user.Id,
				AnimeId = animeId
			};

			await _mediator.Send(command);

			return Ok();
		}
	}
}
