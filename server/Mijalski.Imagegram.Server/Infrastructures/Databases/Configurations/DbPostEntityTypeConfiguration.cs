using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mijalski.Imagegram.Server.Modules.Posts.Databases;

namespace Mijalski.Imagegram.Server.Infrastructures.Databases.Configurations;

class DbPostEntityTypeConfiguration : IEntityTypeConfiguration<DbPost>
{
    public void Configure(EntityTypeBuilder<DbPost> builder)
    {
        builder.ToTable("DbPosts");
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.Comments).WithOne().HasForeignKey(e => e.PostId);
        builder.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId);
    }
}