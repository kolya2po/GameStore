using System.ComponentModel.DataAnnotations;


namespace GameStore.WebApi.Models.Games
{
    public class UpdateGameDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}
