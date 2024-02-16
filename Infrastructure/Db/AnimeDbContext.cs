using Application.Interfaces;
using Domain.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db
{
    public sealed class AnimeDbContext : IdentityDbContext<User>, IAnimeDbContext
    {
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        public AnimeDbContext(DbContextOptions<AnimeDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
