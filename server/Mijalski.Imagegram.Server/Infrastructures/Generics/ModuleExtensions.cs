namespace Mijalski.Imagegram.Server.Infrastructures.Generics;

static class ModuleExtensions
{
    private static readonly List<IModule> RegisteredModules = new ();

    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        foreach (var module in GetModules())
        {
            module.RegisterModule(builder.Services);
            RegisteredModules.Add(module);
        }

        return builder;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        foreach (var module in RegisteredModules)
        {
            module.MapEndpoints(app);
        }

        return app;
    }

    private static IEnumerable<IModule> GetModules()
    {
        return typeof(IModule).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
    }
}