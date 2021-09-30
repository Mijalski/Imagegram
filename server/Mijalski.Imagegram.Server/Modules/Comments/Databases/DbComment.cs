using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Comments.Databases;

class DbComment : ICreationAudited
{
    public Guid Id { get; set; } = default!;
    public string Content { get; set; } = string.Empty;
    public Guid AccountId { get; set; } = default!;
    public virtual DbAccount Account { get; set; } = default!;
    public Guid PostId { get; set; } = default!;
    public DateTimeOffset CreationDateTime { get; set; }
}