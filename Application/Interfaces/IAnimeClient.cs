using Domain.Models.Shiki;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IAnimeClient
	{
		Task<List<AnimeId>> GetTopHundredFullInfoAsync();
        Task<List<Anime>> GetTopHundredAsync();
        Task<AnimeId> GetFullInfoWithIdAsync(int id);
		Task<List<AnimeId>> GetFullInfoWithNameAsync(string value);
        Task<List<Anime>> GetWithNameAsync(string value);
    }
}
