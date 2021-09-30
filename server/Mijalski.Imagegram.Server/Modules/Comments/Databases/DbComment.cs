using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Comments.Databases;

class DbComment : ICreationAudited
{
    public Guid Id { get; } = default!;
    public string Content { get; } = string.Empty;
    public Guid AccountId { get; } = default!;
    public virtual DbAccount Account { get; } = default!;
    public Guid PostId { get; } = default!;
    public DateTimeOffset CreationDateTime { get; set; }
}