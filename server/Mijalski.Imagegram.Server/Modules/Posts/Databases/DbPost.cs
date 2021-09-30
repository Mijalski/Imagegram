using System.Collections.ObjectModel;
using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Domain.Posts;
using Mijalski.Imagegram.Server.Modules.Comments.Databases;

namespace Mijalski.Imagegram.Server.Modules.Posts.Databases;

class DbPost
{
    public Guid Id { get; } = default!;
    public byte[] Image { get; } = default!;
    public string? Caption { get; }
    public Guid AccountId { get; } = default!;
    public virtual Account Account { get; } = default!;
    public DateTime CreationDateTime { get; }
    public virtual ICollection<DbComment> Comments { get; } = new Collection<DbComment>();
}