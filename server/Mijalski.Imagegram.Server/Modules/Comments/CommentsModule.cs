using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Comments.CommandHandlers;
using Mijalski.Imagegram.Server.Modules.Comments.Mappers;
using Mijalski.Imagegram.Server.Modules.Posts.CommandHandlers;

namespace Mijalski.Imagegram.Server.Modules.Comments;

public class CommentsModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services
            .AddTransient<CreateCommentCommandHandler>()
            .AddTransient<ICommentMapper, CommentMapper>();
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/posts/{postId}/comments",
                async (HttpContext context, string postId, CreateCommentRequest request, CreateCommentCommandHandler handler) =>
                {
                    if (request is null || !Guid.TryParse(postId, out var postGuid))
                    {
                        return Results.BadRequest();
                    }

                    await handler.CreateAsync(new CreateCommentCommand(postGuid, request.Content), context.RequestAborted);

                    var id = postGuid;
                    return Results.CreatedAtRoute("GetPost", new { id }, id);
                })
            .WithName("CreateComment")
            .ProducesValidationProblem()
            .RequireAuthorization()
            .Produces<string>(StatusCodes.Status201Created);

        return endpoints;
    }
}