namespace Domain.Models.User
{
	public class Rating
    {
        public int Id { get; set; }
        public int Anime { get; set; }
        public User User { get; set; }
        public int value { get; set; }
    }
}
