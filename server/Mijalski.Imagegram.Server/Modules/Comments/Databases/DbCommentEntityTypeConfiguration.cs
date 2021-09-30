using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mijalski.Imagegram.Server.Modules.Comments.Databases;

class DbCommentEntityTypeConfiguration : IEntityTypeConfiguration<DbComment>
{
    public void Configure(EntityTypeBuilder<DbComment> builder)
    {
        builder.ToTable("DbComments");
        builder.HasKey(e => e.Id);
    }
}