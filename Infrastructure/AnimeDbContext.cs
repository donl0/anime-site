using Application.Interfaces;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
	public class AnimeDbContext : DbContext, IAnimeDbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Rating> Ratings { get; }
		public DbSet<Bookmark> Bookmarks { get; }

		public AnimeDbContext(DbContextOptions<AnimeDbContext> options) :base(options) {

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

		}
	}
}
