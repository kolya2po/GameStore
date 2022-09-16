using System.Collections.Generic;

namespace GameStore.BLL.Models
{
    public class GenreModel : BaseModel
    {
        public int? ParentGenreId { get; set; }
        public string Name { get; set; }
        public IEnumerable<GenreModel> SubGenres { get; set; }
    }
}
