using Mijalski.Imagegram.Domain.Accounts;

namespace Mijalski.Imagegram.Server.Modules.Comments.Databases;

class DbComment
{
    public Guid Id { get; } = default!;
    public string Content { get; } = string.Empty;
    public Guid AccountId { get; } = default!;
    public virtual Account Account { get; } = default!;
}