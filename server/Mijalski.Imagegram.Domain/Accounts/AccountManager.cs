namespace Mijalski.Imagegram.Domain.Accounts;

public interface IAccountManager
{
    ValueTask<Account> CreateAccountAsync(string name, CancellationToken cancellationToken = default);
}

public class AccountManager : IAccountManager
{
    public ValueTask<Account> CreateAccountAsync(string name, CancellationToken cancellationToken = default)
    {
        return new ValueTask<Account>(new Account(name));
    }
}