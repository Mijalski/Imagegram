using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mijalski.Imagegram.Server.Modules.Accounts.Databases;

class DbAccountEntityTypeConfiguration : IEntityTypeConfiguration<DbAccount>
{
    public void Configure(EntityTypeBuilder<DbAccount> builder)
    {
        builder.ToTable("DbAccounts");
        builder.HasKey(e => e.Id);
    }
}