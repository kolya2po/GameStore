using System.ComponentModel.DataAnnotations;

namespace GameStore.WebApi.Models.Games
{
    public class CreateGameDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Author's id is required.")]
        public int AuthorId { get; set; }
    }
}
