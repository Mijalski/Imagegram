using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Domain.Posts;

namespace Mijalski.Imagegram.Domain.Comments;

public interface ICommentManager
{
    ValueTask<Comment> CreateCommentAsync(Account account, Post post, string content, CancellationToken cancellationToken = default);
}

public class CommentManager : ICommentManager
{
    public ValueTask<Comment> CreateCommentAsync(Account account, Post post, string content, CancellationToken cancellationToken = default)
    {
        return new ValueTask<Comment>(new Comment(account, post, content));
    }
}