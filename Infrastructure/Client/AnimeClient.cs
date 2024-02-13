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

		public async Task<List<Anime>> GetTopHundredAsync()
		{
			string url = "animes?order=ranked&limit=10";
			List<Anime> animes = await _httpClient.GetFromJsonAsync<List<Anime>>(url);

			return animes;
		}

		public async Task<Anime> GetWithIdAsync(int id)
		{
			string url = $"animes/{id}";
			Anime animes = await _httpClient.GetFromJsonAsync<Anime>(url);

			return animes;
		}

		public async Task<Anime> GetWithNameAsync(string value)
		{
			int limit = 35;
			string url = $"animes?search={value}&limit={limit}";
			Anime animes = await _httpClient.GetFromJsonAsync<Anime>(url);

			return animes;
		}
	}
}
