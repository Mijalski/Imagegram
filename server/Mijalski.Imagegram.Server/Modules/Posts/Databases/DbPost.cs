using System.Collections.ObjectModel;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;
using Mijalski.Imagegram.Server.Modules.Comments.Databases;

namespace Mijalski.Imagegram.Server.Modules.Posts.Databases;

class DbPost : ICreationAudited
{
    public Guid Id { get; } = default!;
    public byte[] Image { get; } = default!;
    public string? Caption { get; }
    public Guid AccountId { get; } = default!;
    public virtual DbAccount Account { get; } = default!;
    public DateTimeOffset CreationDateTime { get; set; }
    public virtual ICollection<DbComment> Comments { get; } = new Collection<DbComment>();
}