namespace Application.Dtos.Common
{
    public class FilmWatchlistDto
    {
        public int Sequence { get; set; }
        public int FilmId { get; set; }
        public bool IsWatched { get; set; }
    }
}
