using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mijalski.Imagegram.Server.Modules.Comments.Databases;

namespace Mijalski.Imagegram.Server.Infrastructures.Databases.Configurations;

class DbCommentEntityTypeConfiguration : IEntityTypeConfiguration<DbComment>
{
    public void Configure(EntityTypeBuilder<DbComment> builder)
    {
        builder.ToTable("DbComments");
        builder.HasKey(e => e.Id);
    }
}