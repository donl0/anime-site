using System.Collections.Generic;

namespace Domain.Models.User
{
	public class Bookmark
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int> Animes { get; set; }
        public User User { get; set; }
    }
}
