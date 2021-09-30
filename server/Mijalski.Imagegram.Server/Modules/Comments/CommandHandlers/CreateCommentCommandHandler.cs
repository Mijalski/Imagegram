using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using Mijalski.Imagegram.Domain.Comments;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Comments.Databases;
using Mijalski.Imagegram.Server.Modules.Comments.Mappers;
using Mijalski.Imagegram.Server.Modules.Posts.Databases;
using Mijalski.Imagegram.Server.Modules.Posts.Extensions;
using Mijalski.Imagegram.Server.Modules.Posts.Mappers;

namespace Mijalski.Imagegram.Server.Modules.Comments.CommandHandlers;

public record CreateCommentRequest(string Content);
public record CreateCommentCommand(Guid PostId, string Content);

class CreateCommentCommandHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ICommentManager _commentManager;
    private readonly ICommentMapper _mapper;
    private readonly IPostMapper _postMapper;

    public CreateCommentCommandHandler(ApplicationDbContext dbContext,
        ICommentMapper mapper, 
        ICurrentAccountService currentAccountService, 
        ICommentManager commentManager, 
        IPostMapper postMapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currentAccountService = currentAccountService ?? throw new ArgumentNullException(nameof(currentAccountService));
        _commentManager = commentManager ?? throw new ArgumentNullException(nameof(commentManager));
        _postMapper = postMapper ?? throw new ArgumentNullException(nameof(postMapper));
    }

    public async Task CreateAsync(CreateCommentCommand command, CancellationToken cancellationToken = default)
    {
        var currentAccount = await _currentAccountService.GetCurrentAccountAsync(cancellationToken);
        var dbPost = await _dbContext.Set<DbPost>().SingleOrDefaultAsync(p => p.Id == command.PostId, cancellationToken);

        if (dbPost is null)
        {
            throw new ArgumentException("Post with given ID does not exist!");
        }

        var post = _postMapper.Map(dbPost, currentAccount);
        var comment = await _commentManager.CreateCommentAsync(currentAccount, post, command.Content, cancellationToken);
        var dbComment = _mapper.Map(comment, post, currentAccount);

        await _dbContext.Set<DbComment>().AddAsync(dbComment, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}