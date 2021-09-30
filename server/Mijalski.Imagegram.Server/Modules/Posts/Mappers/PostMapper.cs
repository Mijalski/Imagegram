using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Domain.Posts;
using Mijalski.Imagegram.Server.Modules.Posts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Posts.Mappers;

internal interface IPostMapper
{
    Post Map(DbPost dbPost, Account account);
    DbPost Map(Post post, Account account);
}

class PostMapper : IPostMapper
{
    public Post Map(DbPost dbPost, Account account) => new (account, new MemoryStream(dbPost.Image), dbPost.Caption);
    public DbPost Map(Post post, Account account) =>
        new ()
        {
            AccountId = account.Id,
            Caption = post.Caption,
            Image = post.Image.ToArray()
        };
}