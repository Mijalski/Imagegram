using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;
using Mijalski.Imagegram.Server.Modules.Comments.Databases;
using Mijalski.Imagegram.Server.Modules.Posts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Accounts.Databases;

class DbAccount : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<DbComment> Comments { get; set; } = new Collection<DbComment>();
    public virtual ICollection<DbPost> Posts { get; set; } = new Collection<DbPost>();
}