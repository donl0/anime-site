using Application.CQRS.User.Queries;
using Domain.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
		public async Task<IActionResult> BookmarksAsync()
		{
			var http = HttpContext;

				var user = await _userManager.GetUserAsync(HttpContext.User);

			GetUserBookmarksQuery query = new GetUserBookmarksQuery
			{
				UserId = user.Id
			};

			var bookmarks = await _mediator.Send(query);

			return View(bookmarks);
		}
	}
}
