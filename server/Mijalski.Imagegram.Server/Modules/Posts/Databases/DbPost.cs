using System.Collections.ObjectModel;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;
using Mijalski.Imagegram.Server.Modules.Comments.Databases;

namespace Mijalski.Imagegram.Server.Modules.Posts.Databases;

class DbPost : ICreationAudited
{
    public Guid Id { get; set; } = default!;
    public byte[] Image { get; set; } = default!;
    public string? Caption { get; set; }
    public Guid AccountId { get; set; } = default!;
    public virtual DbAccount Account { get; set; } = default!;
    public DateTimeOffset CreationDateTime { get; set; }
    public virtual ICollection<DbComment> Comments { get; set; } = new Collection<DbComment>();
}