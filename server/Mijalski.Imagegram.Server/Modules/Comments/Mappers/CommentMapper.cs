using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Domain.Comments;
using Mijalski.Imagegram.Domain.Posts;
using Mijalski.Imagegram.Server.Modules.Comments.Databases;

namespace Mijalski.Imagegram.Server.Modules.Comments.Mappers;

internal interface ICommentMapper
{
    Comment Map(DbComment dbComment, Post post, Account account);
    DbComment Map(Comment comment, Post post, Account account);
}

class CommentMapper : ICommentMapper
{
    public Comment Map(DbComment dbComment, Post post, Account account) =>
        new(account, post, dbComment.Content);

    public DbComment Map(Comment comment, Post post, Account account) => new()
    {
        Content = comment.Content,
        AccountId = account.Id,
        PostId = post.Id
    };

}