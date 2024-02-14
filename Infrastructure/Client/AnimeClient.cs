using Application.Interfaces;
using Domain.Models.Shiki;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Infrastructure.Client
{
	public sealed class AnimeClient : IAnimeClient
	{
		private readonly HttpClient _httpClient;

		public AnimeClient(HttpClient httpClient) { 
			_httpClient = httpClient;
		}

		public async Task<List<AnimeId>> GetTopHundredFullInfoAsync()
		{
			List<Anime> animes = await GetTopHundredAsync();

            var animesId = await ConvertToAnimesId(animes);

			return animesId;
		}

		public async Task<AnimeId> GetFullInfoWithIdAsync(int id)
		{
			string url = $"animes/{id}";
			AnimeId animes = await _httpClient.GetFromJsonAsync<AnimeId>(url);

			return animes;
		}

		public async Task<List<AnimeId>> GetFullInfoWithNameAsync(string value)
		{
			List<Anime> animes = await GetWithNameAsync(value);

            var animesId = await ConvertToAnimesId(animes);

			return animesId;
		}

		private async Task<List<AnimeId>> ConvertToAnimesId(List<Anime> animes) {

			List<AnimeId> animesId = new List<AnimeId>();

			foreach (Anime anime in animes)
			{
				var animeId = await GetFullInfoWithIdAsync(anime.Id);

				animesId.Add(animeId);
			}

			return animesId;
		}

        public async Task<List<Anime>> GetTopHundredAsync()
        {
            string url = "animes?order=ranked&limit=30";

            List<Anime> animes = await _httpClient.GetFromJsonAsync<List<Anime>>(url);

			return animes;
        }

        public async Task<List<Anime>> GetWithNameAsync(string value)
        {
            int limit = 30;
            string url = $"animes?order=ranked&search={value}&limit={limit}";
            List<Anime> animes = await _httpClient.GetFromJsonAsync<List<Anime>>(url);

			return animes;
        }
    }
}
