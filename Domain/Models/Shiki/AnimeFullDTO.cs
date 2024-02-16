using System;

namespace Domain.Models.Shiki
{
	public class AnimeFullDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Russian { get; set; }
		public Image Image { get; set; }
		public string Url { get; set; }
		public string Kind { get; set; }
		public string Score { get; set; }
		public string Status { get; set; }
		public int Episodes { get; set; }
		public int EpisodesAired { get; set; }
		public string AiredOn { get; set; }
		public string ReleasedOn { get; set; }
		public string Rating { get; set; }
		public string[] English { get; set; }
		public string[] Japanese { get; set; }
		public string[] Synonyms { get; set; }
		public object LicenseNameRu { get; set; }
		public int Duration { get; set; }
		public string Description { get; set; }
		public string DescriptionHtml { get; set; }
		public object DescriptionSource { get; set; }
		public string Franchise { get; set; }
		public bool Favoured { get; set; }
		public bool Anons { get; set; }
		public bool Ongoing { get; set; }
		public int ThreadId { get; set; }
		public int TopicId { get; set; }
		public int MyanimelistId { get; set; }
		public Rates_Scores_Stats[] RatesScoresStats { get; set; }
		public Rates_Statuses_Stats[] RatesStatusesStats { get; set; }
		public DateTime UpdatedAt { get; set; }
		public object NextEpisodeAt { get; set; }
		public string[] Fansubbers { get; set; }
		public string[] Fandubbers { get; set; }
		public object[] Licensors { get; set; }
		public Genre[] Genres { get; set; }
		public Studio[] Studios { get; set; }
		public Video[] Videos { get; set; }
		public Screenshot[] Screenshots { get; set; }
		public object UserRate { get; set; }

		public class Rates_Scores_Stats
		{
			public int Name { get; set; }
			public int Value { get; set; }
		}

		public class Rates_Statuses_Stats
		{
			public string Name { get; set; }
			public int Value { get; set; }
		}

		public class Studio
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string FilteredName { get; set; }
			public bool Real { get; set; }
			public string Image { get; set; }
		}

		public class Video
		{
			public int Id { get; set; }
			public string Url { get; set; }
			public string ImageUrl { get; set; }
			public string PlayerUrl { get; set; }
			public string Name { get; set; }
			public string Kind { get; set; }
			public string Hosting { get; set; }
		}

		public class Screenshot
		{
			public string Original { get; set; }
			public string Preview { get; set; }
		}
	}
}
