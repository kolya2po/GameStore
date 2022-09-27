using GameStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.RelationshipsConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne<Comment>()
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId);

            builder.HasOne<Game>()
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.GameId);

            builder.HasOne(c => c.Author)
                .WithMany(c => c.CreatedComments)
                .HasForeignKey(c => c.AuthorId);
        }
    }
}
