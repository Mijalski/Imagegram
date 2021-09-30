using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Accounts.Passwords;

class AccountBCryptPasswordService : IAccountPasswordService
{
    public string CreatePasswordHash(string input)
    {
        return BCrypt.Net.BCrypt.HashPassword(input);
    }

    public bool VerifyPasswordHash(DbAccount dbAccount, string input)
    {
        return BCrypt.Net.BCrypt.Verify(input, dbAccount.PasswordHash);
    }
}