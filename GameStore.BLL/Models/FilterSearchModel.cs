using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models
{
    public class FilterSearchModel
    {
        public IEnumerable<string> Genres { get; set; }

        [MinLength(3, ErrorMessage = "Name must contain at least 3 characters.")]
        public string GameName { get; set; }
    }
}
