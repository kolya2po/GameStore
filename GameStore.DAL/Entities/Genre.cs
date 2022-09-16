using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Genre : BaseEntity
    {
        public int? ParentGenreId { get; set; }
        public string Name { get; set; }
        public IEnumerable<GameGenre> Games { get; set; }
        public IEnumerable<Genre> SubGenres { get; set; }
    }
}
