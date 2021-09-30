using Mijalski.Imagegram.Domain.Accounts;
using System.Collections.ObjectModel;

namespace Mijalski.Imagegram.Domain.Posts;

public class Post
{
    public Guid Guid { get; }
    public byte[] Image { get; } = default!;
    public string? Caption { get; }
    public Guid AccountId { get; }
    public virtual Account Account { get; } = default!;
    public DateTime CreationDateTime { get; }
    public virtual ICollection<Comment> Comments { get; } = new Collection<Comment>();
}