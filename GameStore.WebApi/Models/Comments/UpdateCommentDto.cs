using System.ComponentModel.DataAnnotations;

namespace GameStore.WebApi.Models.Comments
{
    public class UpdateCommentDto
    {
        [Required(ErrorMessage = "Comment's id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment's text is required.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Author's name is required.")]
        public string Author { get; set; }
        public int? ParentCommentId { get; set; }

        [Required(ErrorMessage = " Game's id is required.")]
        public int GameId { get; set; }
    }
}
