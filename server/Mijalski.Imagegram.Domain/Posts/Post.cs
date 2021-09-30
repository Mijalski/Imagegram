using Mijalski.Imagegram.Domain.Accounts;
using System.Collections.ObjectModel;

namespace Mijalski.Imagegram.Domain.Posts;

public class Post
{
    public Post(Account account, MemoryStream image, string? caption)
    {
        Image = image;
        Caption = caption;
        Account = account;
        Comments = new List<Comment>();
    }

    public virtual Account Account { get; }
    public MemoryStream Image { get; }
    public string? Caption { get; }
    public virtual ICollection<Comment> Comments { get; }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }
}