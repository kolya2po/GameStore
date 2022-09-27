using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Game : BaseEntity
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<GameGenre> Genres { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
