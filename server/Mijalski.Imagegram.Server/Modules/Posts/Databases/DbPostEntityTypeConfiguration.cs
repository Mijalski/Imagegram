using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mijalski.Imagegram.Server.Modules.Posts.Databases;

class DbPostEntityTypeConfiguration : IEntityTypeConfiguration<DbPost>
{
    public void Configure(EntityTypeBuilder<DbPost> builder)
    {
        builder.ToTable("DbPosts");
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.Comments).WithOne().HasForeignKey(e => e.PostId);
        builder.HasOne(e => e.Account).WithMany(e => e.Posts).HasForeignKey(e => e.AccountId).OnDelete(DeleteBehavior.Cascade);
    }
}