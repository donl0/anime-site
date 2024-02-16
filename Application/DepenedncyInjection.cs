using Application.Models;
using Domain.Models.Shiki;
using Domain.Models.User;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
	public static class DepenedncyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services) {
			services.AddMediatR(Assembly.GetExecutingAssembly());

            RegisterMapper(services);

            return services;
		}

		private static IServiceCollection RegisterMapper(IServiceCollection services) {
            var config = new TypeAdapterConfig();

            const string shikiUrl = "https://shikimori.one";

            config.NewConfig<AnimeFullDTO, AnimePageVM>()
                .Map(dest => dest.Image.Original, src => $"{shikiUrl}{src.Image.Original}")
                .Map(dest => dest.Image.Preview, src => $"{shikiUrl}{src.Image.Preview}")
                .Map(dest => dest.Image.X96, src => $"{shikiUrl}{src.Image.X96}")
                .Map(dest => dest.Image.X48, src => $"{shikiUrl}{src.Image.X48}")
                .RequireDestinationMemberSource(true);

            config.NewConfig<Anime, AnimePageVM>()
                .Map(dest => dest.Image.Original, src => $"{shikiUrl}{src.Image.Original}")
                .Map(dest => dest.Image.Preview, src => $"{shikiUrl}{src.Image.Preview}")
                .Map(dest => dest.Image.X96, src => $"{shikiUrl}{src.Image.X96}")
                .Map(dest => dest.Image.X48, src => $"{shikiUrl}{src.Image.X48}")
                .RequireDestinationMemberSource(true);

            config.NewConfig<AnimeFullDTO, AnimePageDTO>()
                .Map(dest => dest.Image.Original, src => $"{shikiUrl}{src.Image.Original}")
				.Map(dest => dest.Image.Preview, src => $"{shikiUrl}{src.Image.Preview}")
				.Map(dest => dest.Image.X96, src => $"{shikiUrl}{src.Image.X96}")
				.Map(dest => dest.Image.X48, src => $"{shikiUrl}{src.Image.X48}")
				.RequireDestinationMemberSource(true);

			config.NewConfig<Bookmark, UserBookmarkVM>()
	        .Map(dest => dest.Title, src => src.Title)
	        .Map(dest => dest.Count, src => src.Animes != null ? src.Animes.Count : 0);


			services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
