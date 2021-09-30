using Mijalski.Imagegram.Domain.Accounts;

namespace Mijalski.Imagegram.Domain.Posts;

public interface IPostManager
{
    ValueTask<Post> CreatePostAsync(Account account, MemoryStream image, string? caption, CancellationToken cancellationToken = default);
}

public class PostManager : IPostManager
{
    public ValueTask<Post> CreatePostAsync(Account account, MemoryStream image, string? caption, CancellationToken cancellationToken = default)
    {
        return new ValueTask<Post>(new Post(account, image, caption));
    }
}