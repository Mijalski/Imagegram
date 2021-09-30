using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Domain.Comments;
using Mijalski.Imagegram.Domain.Posts;

namespace Mijalski.Imagegram.Server.Infrastructures.Generics;

public static class DomainDependencyInjectionExtensions
{
    public static IServiceCollection RegisterDomain(this WebApplicationBuilder builder)
    {
        return builder.Services.AddTransient<IAccountManager, AccountManager>()
            .AddTransient<IPostManager, PostManager>()
            .AddTransient<ICommentManager, CommentManager>();
    }

}