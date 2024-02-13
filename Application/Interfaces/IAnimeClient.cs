using Domain.Models.Shiki;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IAnimeClient
	{
		Task<List<Anime>> GetTopHundredAsync();
		Task<Anime> GetWithIdAsync(int id);
		Task<Anime> GetWithNameAsync(string value);
	}
}
