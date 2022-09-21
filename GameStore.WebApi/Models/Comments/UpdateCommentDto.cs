using System.ComponentModel.DataAnnotations;

namespace GameStore.WebApi.Models.Comments
{
    public class UpdateCommentDto
    {
        [Required(ErrorMessage = "Comment's id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment's text is required.")]
        public string Text { get; set; }
    }
}
