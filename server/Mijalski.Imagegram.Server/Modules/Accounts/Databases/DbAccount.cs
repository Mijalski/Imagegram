using Microsoft.AspNetCore.Identity;

namespace Mijalski.Imagegram.Server.Modules.Accounts.Databases;

class DbAccount : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
}