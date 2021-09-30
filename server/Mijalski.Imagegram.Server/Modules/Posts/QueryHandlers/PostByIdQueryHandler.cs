using Microsoft.EntityFrameworkCore;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Modules.Posts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Posts.QueryHandlers;

public record PostDto(byte[] Image, string? Caption, IEnumerable<string> Comments);

class PostByIdQueryHandler
{
    private readonly DbSet<DbPost> _dbPosts;

    public PostByIdQueryHandler(ApplicationDbContext dbContext)
    {
        _dbPosts = dbContext.Set<DbPost>() ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<PostDto?> GetPostOrDefaultById(Guid id, CancellationToken cancellationToken = default)
    {
        var dbPost = await _dbPosts
            .Include(_ => _.Comments)
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);

        return dbPost is null ? null : new PostDto(dbPost.Image, dbPost.Caption, dbPost.Comments.Select(c => c.Content));
    }
}
