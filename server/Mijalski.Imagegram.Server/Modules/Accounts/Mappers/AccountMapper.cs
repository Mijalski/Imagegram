using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Accounts.Mappers;

internal interface IAccountMapper
{
    Account Map(DbAccount dbAccount);
    DbAccount Map(Account account);
}

class AccountMapper : IAccountMapper
{
    public Account Map(DbAccount dbAccount) =>
        new (dbAccount.Id, dbAccount.Name);
    public DbAccount Map(Account account) => new()
    {
        Name = account.Name
    };

}