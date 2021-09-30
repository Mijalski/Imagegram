using Microsoft.AspNetCore.Identity;
using Mijalski.Imagegram.Domain.Accounts;

namespace Mijalski.Imagegram.Server.Modules.Accounts.Databases;

class DbAccount : IdentityUser<Guid>
{
    public string Name { get; } = string.Empty;
}