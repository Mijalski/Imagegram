using System.Net.Mime;
using Mijalski.Imagegram.Domain.Posts;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;
using Mijalski.Imagegram.Server.Modules.Posts.Databases;
using Mijalski.Imagegram.Server.Modules.Posts.Extensions;
using Mijalski.Imagegram.Server.Modules.Posts.Mappers;

namespace Mijalski.Imagegram.Server.Modules.Posts.CommandHandlers;

public record CreatePostCommand(byte[] Image, string? Caption);

class CreatePostCommandHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IPostManager _postManager;
    private readonly IPostMapper _mapper;

    public CreatePostCommandHandler(ApplicationDbContext dbContext,
        IPostMapper mapper, 
        ICurrentAccountService currentAccountService, 
        IPostManager postManager)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currentAccountService = currentAccountService ?? throw new ArgumentNullException(nameof(currentAccountService));
        _postManager = postManager ?? throw new ArgumentNullException(nameof(postManager));
    }

    public async Task<Guid> CreateAsync(CreatePostCommand command, CancellationToken cancellationToken = default)
    {
        var imageFormat = ImageFormatExtensions.GetImageFormat(command.Image);

        if (imageFormat == ImageFormat.Unknown)
        {
            throw new ArgumentException("Image format not recognized!");
        }

        if (imageFormat == ImageFormat.Png)
        {
            throw new NotImplementedException();
        } 
        else if (imageFormat == ImageFormat.Bmp)
        {
            throw new NotImplementedException();
        }

        var currentAccount = await _currentAccountService.GetCurrentAccountAsync(cancellationToken);
        await using var imageMemoryStream = new MemoryStream(command.Image);

        var post = await _postManager.CreatePostAsync(currentAccount, imageMemoryStream, command.Caption, cancellationToken);
        var dbPost = _mapper.Map(post, currentAccount);

        await _dbContext.Set<DbPost>().AddAsync(dbPost, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return dbPost.Id;
    }
}