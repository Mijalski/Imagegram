using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Domain.Posts;

namespace Mijalski.Imagegram.Domain.Comments;

public class Comment
{
    public Comment(Account account, Post post, string content)
        : this(Guid.Empty, account, post, content)
    {
    }

    public Comment(Guid id, Account account, Post post, string content)
    {
        Id = id;
        Account = account;
        Post = post;
        Content = content;
    }

    public Guid Id {  get; set; }
    public Account Account { get; }
    public Post Post { get; }
    public string Content { get; }
}