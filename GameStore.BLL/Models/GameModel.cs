using System.Collections.Generic;

namespace GameStore.BLL.Models
{
    public class GameModel : BaseModel
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}
