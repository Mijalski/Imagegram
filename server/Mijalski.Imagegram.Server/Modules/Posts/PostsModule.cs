using Mijalski.Imagegram.Domain.Posts;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Posts.CommandHandlers;
using Mijalski.Imagegram.Server.Modules.Posts.Mappers;
using Mijalski.Imagegram.Server.Modules.Posts.QueryHandlers;

namespace Mijalski.Imagegram.Server.Modules.Posts;

public class PostsModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services
            .AddTransient<CreatePostCommandHandler>()
            .AddTransient<PostByIdQueryHandler>()
            .AddTransient<AllPostsQueryHandler>()
            .AddTransient<IPostMapper, PostMapper>();
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/posts/{id}",
                async (HttpContext context, Guid id, PostByIdQueryHandler handler) =>
                {
                    var postDto = await handler.GetPostOrDefaultById(id, context.RequestAborted);
                    return postDto is null ? Results.NotFound() : Results.Ok(postDto);
                })
            .WithName("GetPost")
            .Produces<PostDto>()
            .Produces(StatusCodes.Status404NotFound);

        endpoints.MapGet("/posts",
                async (HttpContext context, int? skipCount, int? takeCount, AllPostsQueryHandler handler) =>
                {
                    return Results.Ok(await handler.GetAllPostsFromTo(skipCount, takeCount, context.RequestAborted));
                })
            .WithName("GetAllPosts")
            .Produces<IList<PostDto>>();

        endpoints.MapPost("/posts",
                async (HttpContext context, CreatePostCommand command, CreatePostCommandHandler handler) =>
                {
                    if (command is null)
                    {
                        return Results.BadRequest();
                    }

                    var id = await handler.CreateAsync(command, context.RequestAborted);

                    return Results.CreatedAtRoute("GetPost", new { id }, id);
                })
            .WithName("CreatePost")
            .ProducesValidationProblem()
            .RequireAuthorization()
            .Produces<string>(StatusCodes.Status201Created);

        return endpoints;
    }
}