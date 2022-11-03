using System.Collections.Generic;

namespace GameStore.BLL.Models
{
    public class CommentModel : BaseModel
    {
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public int? ParentCommentId { get; set; }
        public int AuthorId { get; set; }
        public IEnumerable<CommentModel> Replies { get; set; }
    }
}
