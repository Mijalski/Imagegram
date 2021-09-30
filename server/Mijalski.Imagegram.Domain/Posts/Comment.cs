using Mijalski.Imagegram.Domain.Accounts;

namespace Mijalski.Imagegram.Domain.Posts;

public class Comment
{
    public Guid Id {  get; }
    public string Content { get; } = string.Empty;
    public Guid AccountId { get; }= default!;
    public virtual Account Accounts {  get; } = default!;
}