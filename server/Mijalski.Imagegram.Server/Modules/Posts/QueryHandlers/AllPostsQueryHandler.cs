using Microsoft.EntityFrameworkCore;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Modules.Posts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Posts.QueryHandlers;

public record PostDto(Guid id, byte[] Image, string? Caption, IEnumerable<string> Comments);

class AllPostsQueryHandler
{
    private readonly DbSet<DbPost> _dbPosts;

    public AllPostsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbPosts = dbContext.Set<DbPost>() ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<List<PostDto>> GetAllPostsFromTo(int? skipCount, int? takeCount, CancellationToken cancellationToken = default) =>
        _dbPosts
            .Include(_ => _.Comments)
            .OrderByDescending(_ => _.Comments.Count)
            .Skip(skipCount ?? 0)
            .Take(takeCount ?? 10)
            .Select(p => new PostDto(p.Id, p.Image, p.Caption,
                p.Comments.OrderByDescending(c => c.CreationDateTime).Take(2).Select(c => c.Content)))
            .ToListAsync(cancellationToken);
}
