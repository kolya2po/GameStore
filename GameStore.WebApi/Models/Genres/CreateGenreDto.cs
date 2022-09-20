namespace GameStore.WebApi.Models.Genres
{
    public class CreateGenreDto
    {
        public int? ParentGenreId { get; set; }
        public string Name { get; set; }
    }
}
