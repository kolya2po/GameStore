using System;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public string Author { get; set; }
        public int GameId { get; set; }
        public int? ParentCommentId { get; set; }
        public IEnumerable<Comment> Replies { get; set; }
    }
}
