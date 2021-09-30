using Mijalski.Imagegram.Domain.Accounts;
using System.Collections.ObjectModel;
using Mijalski.Imagegram.Domain.Comments;

namespace Mijalski.Imagegram.Domain.Posts;

public class Post
{
    public Post(Account account, MemoryStream image, string? caption)
        : this(Guid.Empty, account, image, caption)
    {
    }

    public Post(Guid id, Account account, MemoryStream image, string? caption)
    {
        Id = id;
        Image = image;
        Caption = caption;
        Account = account;
        Comments = new List<Comment>();
    }
    
    public virtual Guid Id { get; }
    public virtual Account Account { get; }
    public MemoryStream Image { get; }
    public string? Caption { get; }
    public virtual ICollection<Comment> Comments { get; }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }
}