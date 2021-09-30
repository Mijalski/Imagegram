using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Accounts.Passwords;

interface IAccountPasswordService
{
    string CreatePasswordHash(string input);
    bool VerifyPasswordHash(DbAccount dbAccount, string input);
}