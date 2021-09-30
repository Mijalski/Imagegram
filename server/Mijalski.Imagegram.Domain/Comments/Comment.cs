using Mijalski.Imagegram.Domain.Accounts;

namespace Mijalski.Imagegram.Domain.Posts;

public class Comment
{
    public Comment(Account account, string content)
    {
        Account = account;
        Content = content;
    }

    public Account Account { get; }
    public string Content { get; }
}