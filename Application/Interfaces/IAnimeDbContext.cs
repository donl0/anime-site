using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IAnimeDbContext
	{
		DbSet<User> Users { get; }
		DbSet<Rating> Ratings { get;}
		DbSet<Bookmark> Bookmarks { get;}

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
