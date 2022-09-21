using System.ComponentModel.DataAnnotations;

namespace GameStore.WebApi.Models.Comments
{
    public class CreateCommentDto
    {
        [Required(ErrorMessage = "Comment's text is required.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Game's id is required.")]
        public int GameId { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
