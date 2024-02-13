namespace Domain.Models.User
{
	public class Rating
    {
        public int Id { get; set; }
        public Anime Anime { get; set; }
        public User User { get; set; }
    }
}
