using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Infrastructures.Databases.Configurations;

class DbAccountEntityTypeConfiguration : IEntityTypeConfiguration<DbAccount>
{
    public void Configure(EntityTypeBuilder<DbAccount> builder)
    {
        builder.ToTable("DbAccounts");
        builder.HasKey(e => e.Id);
    }
}