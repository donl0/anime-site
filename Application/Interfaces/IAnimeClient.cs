using Domain.Models.Shiki;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IAnimeClient
	{
		Task<List<AnimeFullDTO>> GetTopHundredFullInfoAsync();
        Task<List<Anime>> GetTopHundredAsync();
        Task<AnimeFullDTO> GetFullInfoWithIdAsync(int id);
		Task<List<AnimeFullDTO>> GetFullInfoWithNameAsync(string value);
        Task<List<Anime>> GetWithNameAsync(string value);
    }
}
