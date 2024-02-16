using Application.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.CQRS.User.Queries
{
    public class GetUserBookmarksQuery : IRequest<ICollection<UserBookmarkVM>>
    {
        public string UserId { get; set; }
    }
}
