using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Accounts.Mappers;

class AccountMapper : IMapper<Account, DbAccount>
{
    public Account Map(DbAccount dbAccount) =>
        new (dbAccount.Name);
    public DbAccount Map(Account account) => new()
    {
        Name = account.Name
    };

}